using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Path = System.IO.Path;
using SoundGenerator;
using SoundGenerator.FileConverter;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoundGeneratorUI
{
    /// <summary>
    /// Interaction logic for FileConverter.xaml
    /// </summary>
    public partial class FileConverter : Page
    {
        public FileConverter()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(FileConverter_Loaded);
        }

        void FileConverter_Loaded(object sender, RoutedEventArgs e)
        {
            formatListBox.Items.Clear();
            String[] formats = FileConvert.FFMPEG_SUPPORTED_FORMATS.Concat(FileConvert.FFMPEG_CODEC_FILE_EXTENSION.Keys).ToArray();
            foreach (String format in formats)
            {
                if (format.Equals("aac"))
                {
                    continue;
                }
                formatListBox.Items.Add(format);
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
            String inputFilePath = inputFileTxtBox.Text;
            if (!ValidateFilePath(inputFilePath))
            {
                return;
            }
            String outputFilePath = outputFileTxtBox.Text;
            if (!ValidateFilePath(outputFilePath))
            {
                return;
            }
            String selectedFormat;
            try
            {
                selectedFormat = formatListBox.Items.GetItemAt(formatListBox.SelectedIndex) as String;
            }
            catch (Exception)
            {
                errorLabel.Visibility = Visibility.Visible;
                errorLabel.Content = "You must select a format!";
                return;
            }
            inputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + inputFilePath + ".aac";
            if (!File.Exists(inputFilePath))
            {
                errorLabel.Visibility = Visibility.Visible;
                errorLabel.Content = "The input file must exist!";
                return;
            }
            outputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + outputFilePath + "." + selectedFormat;
            errorLabel.Visibility = Visibility.Hidden;
            Environment.CurrentDirectory = Path.GetTempPath();
            Program.ExtractAllResources();
            FileConvert.ConvertAAC(inputFilePath, outputFilePath, selectedFormat);
            Thread.Sleep(1000);
            Program.KillAllExternalResources();
        }

        private Boolean ValidateFilePath(String filePath)
        {
            if (filePath.Trim().Equals(""))
            {
                errorLabel.Content = "You must enter a valid file name!";
                errorLabel.Visibility = Visibility.Visible;
                return false;
            }
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalid in invalidChars)
            {
                if (filePath.Contains(invalid))
                {
                    errorLabel.Content = "You must enter a valid file name!";
                    errorLabel.Visibility = Visibility.Visible;
                    return false;
                }
            }
            if (filePath.Length > 200)
            {
                errorLabel.Content = "You must enter a valid file name!";
                errorLabel.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }
    }
}
