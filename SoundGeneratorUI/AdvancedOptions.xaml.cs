using System;
using System.Linq;
using System.Diagnostics;
using Path = System.IO.Path;
using SoundGenerator;
using SoundGenerator.CommandREPL;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoundGeneratorUI
{
    /// <summary>
    /// Interaction logic for AdvancedOptions.xaml
    /// </summary>
    public partial class AdvancedOptions : Page
    {
        public AdvancedOptions()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(AdvancedOptions_Loaded);
        }

        void AdvancedOptions_Loaded(object sender, RoutedEventArgs e)
        {
            errorLabel.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void advancedOptionsEnable_Checked(object sender, RoutedEventArgs e)
        {
            submitButton.Visibility = Visibility.Visible;
            midiFileInstructions.Visibility = Visibility.Visible;
            midiFileNameTxtbox.Visibility = Visibility.Visible;
            midiNoteInstructions.Visibility = Visibility.Visible;
            midiNotesTxtBox.Visibility = Visibility.Visible;
            midiRandomIntroduce.Visibility = Visibility.Visible;
            consoleVersionLabel.Visibility = Visibility.Visible;
            openConsole.Visibility = Visibility.Visible;
        }

        private void advancedOptionsEnable_Unchecked(object sender, RoutedEventArgs e)
        {
            errorLabel.Visibility = Visibility.Hidden;
            submitButton.Visibility = Visibility.Hidden;
            midiFileInstructions.Visibility = Visibility.Hidden;
            midiFileNameTxtbox.Visibility = Visibility.Hidden;
            midiNoteInstructions.Visibility = Visibility.Hidden;
            midiNotesTxtBox.Visibility = Visibility.Hidden;
            midiRandomIntroduce.Visibility = Visibility.Hidden;
            consoleVersionLabel.Visibility = Visibility.Hidden;
            openConsole.Visibility = Visibility.Hidden;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            Action<object, RoutedEventArgs> impl = new Action<object, RoutedEventArgs>(submitButton_Click_Impl);
            Dispatcher.BeginInvoke(impl, DispatcherPriority.ContextIdle, sender, e);
        }

        private void submitButton_Click_Impl(object sender, RoutedEventArgs e)
        {
            String filePath = midiFileNameTxtbox.Text;
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
            ushort notes;
            if (UInt16.TryParse(midiNotesTxtBox.Text, out notes) == false)
            {
                errorLabel.Content = "You must enter a valid note number!";
                errorLabel.Visibility = Visibility.Visible;
                return;
            }
            errorLabel.Visibility = Visibility.Hidden;
            SoundShell shell = new SoundShell();
            shell.TryExecute("random-midi-experimental "+notes.ToString()+" "+filePath);
        }

        private void openConsole_Click(object sender, RoutedEventArgs e)
        {
            Action<object, RoutedEventArgs> impl = new Action<object, RoutedEventArgs>(openConsole_Click_Impl);
            Dispatcher.BeginInvoke(impl, DispatcherPriority.ContextIdle, sender, e);
        }

        private void openConsole_Click_Impl(object sender, RoutedEventArgs e)
        {
            Type consoleProgramType = typeof(Program);
            String consoleProgramFilePath = consoleProgramType.Assembly.Location;
            ProcessStartInfo console_inf = new ProcessStartInfo();
            console_inf.Arguments = "";
            console_inf.FileName = consoleProgramFilePath;
            Process console = Process.Start(console_inf);
        }
    }
}
