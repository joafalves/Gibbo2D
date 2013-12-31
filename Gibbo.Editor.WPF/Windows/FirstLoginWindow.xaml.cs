using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for FirstLoginWindow.xaml
    /// </summary>
    public partial class FirstLoginWindow : Window
    {
        private bool canClose = false;

        public FirstLoginWindow()
        {
            InitializeComponent();
        }

        private void continueBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidEmail(emailTxtBox.Text))
            {
                Properties.Settings.Default.UserEmail = emailTxtBox.Text;
                Properties.Settings.Default.Save();

                canClose = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Email not valid", "Error");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!canClose)
                System.Environment.Exit(0);
        }

        private bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return System.Text.RegularExpressions.Regex.IsMatch(strIn,
                    @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
    }
}
