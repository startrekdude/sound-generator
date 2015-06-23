using System;
using System.Linq;
using System.Threading;
using Path = System.IO.Path;
using SoundGenerator;
using SoundGenerator.WaveCreator;
using SoundGenerator.AACEncoder;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace SoundGeneratorUI
{
    /// <summary>
    /// Interaction logic for GenerateRandomSound.xaml
    /// </summary>
    public partial class GenerateRandomSound : Page
    {

        public GenerateRandomSound()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(GenerateRandomSound_Loaded);
        }

        private void  GenerateRandomSound_Loaded(object sender, RoutedEventArgs e)
        {
            qualityListBox.Items.Clear();
            String[] qualities = Enum.GetNames(typeof(WaveSampleType));
            foreach (String quality in qualities)
            {
                qualityListBox.Items.Add(quality);
            }
            errorLabel.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            Action<object, RoutedEventArgs> impl = new Action<object, RoutedEventArgs>(submitButton_Click_Impl);
            Dispatcher.BeginInvoke(impl, DispatcherPriority.ContextIdle, sender, e);
        }

        private void submitButton_Click_Impl(object sender, RoutedEventArgs e)
        {
            String filePath = fileNameTxtBox.Text;
            if (filePath.Trim().Equals(""))
            {
                errorLabel.Content = "You must enter a valid file name!";
                errorLabel.Visibility = Visibility.Visible;
                return;
            }
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalid in invalidChars)
            {
                if (filePath.Contains(invalid))
                {
                    errorLabel.Content = "You must enter a valid file name!";
                    errorLabel.Visibility = Visibility.Visible;
                    return;

                }
            }
            if (filePath.Length > 200)
            {
                errorLabel.Content = "You must enter a valid file name!";
                errorLabel.Visibility = Visibility.Visible;
                return;
            }
            filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + filePath + ".aac";
            ushort seconds;
            if (UInt16.TryParse(lengthTxtBox.Text, out seconds) == false)
            {
                errorLabel.Visibility = Visibility.Visible;
                errorLabel.Content = "You must enter a valid length!";
                return;
            }
            String selectedQuality;
            try
            {
                selectedQuality = qualityListBox.Items.GetItemAt(qualityListBox.SelectedIndex) as String;
            }
            catch (Exception)
            {
                errorLabel.Visibility = Visibility.Visible;
                errorLabel.Content = "You must select a quality!";
                return;
            }
            WaveSampleType quality;
            if (Enum.TryParse<WaveSampleType>(selectedQuality, out quality) == false)
            {
                errorLabel.Visibility = Visibility.Visible;
                errorLabel.Content = "You must select a valid quality!";
                return;
            }
            errorLabel.Visibility = Visibility.Hidden;
            Environment.CurrentDirectory = Path.GetTempPath();
            Program.ExtractAllResources();
            RandomAACGenerator gen = new RandomAACGenerator(quality, seconds, filePath);
            gen.GenerateAndSave();
            Thread.Sleep(1000);
            Program.KillAllExternalResources();
        }
    }
}
