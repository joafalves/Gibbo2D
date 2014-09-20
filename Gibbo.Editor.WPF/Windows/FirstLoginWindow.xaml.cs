#region Copyrights
/*
Gibbo2D License - Version 1.0
Copyright (c) 2013 - Gibbo2D Team
Founders Joao Alves <joao.cpp.sca@gmail.com> & Luis Fernandes <luisapidcloud@hotmail.com>

Permission is granted to use this software and associated documentation files (the "Software") free of charge, 
to any person or company. The code can be used, modified and merged without restrictions, but you cannot sell 
the software itself and parts where this license applies. Still, permission is granted for anyone to sell 
applications made using this software (for example, a game). This software cannot be claimed as your own, 
except for copyright holders. This license notes should also be available on any of the changed or added files.

The software is provided "as is", without warranty of any kind, express or implied, including but not limited to 
the warranties of merchantability, fitness for a particular purpose and non-infrigement. In no event shall the 
authors or copyright holders be liable for any claim, damages or other liability.

The license applies to all versions of the software, both newer and older than the one listed, unless a newer copy 
of the license is available, in which case the most recent copy of the license supercedes all others.

*/
#endregion

using Facebook;
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

        private void skipBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.UserEmail = Gibbo.Library.GibboHelper.EncryptMD5(DateTime.Now.ToString() + Gibbo.Library.GibboHelper.RandomNumber(1, 1000));
            Properties.Settings.Default.Save();

            canClose = true;
            this.Close();
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

        private void fbBtn_Click(object sender, RoutedEventArgs e)
        {
            var fbLoginDialog = new FacebookLoginWindow(Properties.Settings.Default.AppID, Properties.Settings.Default.ExtendedPermissions);
            fbLoginDialog.ShowDialog();

            if (fbLoginDialog.DialogResult.HasValue && fbLoginDialog.DialogResult.Value)
                DisplayAppropriateMessage(fbLoginDialog.FacebookOAuthResult);
        }

        private void DisplayAppropriateMessage(FacebookOAuthResult facebookOAuthResult)
        {
            if (facebookOAuthResult != null)
            {
                if (facebookOAuthResult.IsSuccess)
                {
                    Properties.Settings.Default.AccessToken = facebookOAuthResult.AccessToken;
                    Properties.Settings.Default.Save();

                    var fb = new FacebookClient(facebookOAuthResult.AccessToken);

                    dynamic result = fb.Get("/me");
                    var name = result.name;

                    // Note, even if you request the email permission it is not guaranteed you will get an email address. 
                    // For example, if someone signed up for Facebook with a phone number instead of an email address, the email field may be empty.
                    var email = result.email;
                    if (IsValidEmail(email))
                    {
                        Properties.Settings.Default.UserEmail = email;
                        Properties.Settings.Default.Save();
                    }

                    // TODO set logout button visibility (mainwindow) to visible
                    // btnLogout = true;

                    canClose = true;
                    this.Close();

                    // for .net 3.5
                    //var result = (IDictionary<string, object>)fb.Get("/me");
                    //var name = (string)result["name"];
                }
                else
                {
                    MessageBox.Show(facebookOAuthResult.ErrorDescription);
                }
            }
        }
    }
}
