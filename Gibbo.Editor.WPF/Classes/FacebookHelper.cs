using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gibbo.Editor.WPF
{
    internal static class FacebookHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return System.Text.RegularExpressions.Regex.IsMatch(strIn,
                    @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facebookOAuthResult"></param>
        public static void DisplayAppropriateMessage(FacebookOAuthResult facebookOAuthResult)
        {
            if (facebookOAuthResult != null)
            {
                if (facebookOAuthResult.IsSuccess)
                {
                    Properties.Settings.Default.AccessToken = facebookOAuthResult.AccessToken;
                    Properties.Settings.Default.Save();

                    // probably return facebookClient?
                    var fb = new FacebookClient(facebookOAuthResult.AccessToken);

                    dynamic result = fb.Get("/me");
                    var name = result.name;

                    string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture", result.id);
                    // var result = fb.Get("me", new { fields = new [] { "id", "name", "picture" } });

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

                    //canClose = true;
                    //this.Close();

                    // for .net 3.5
                    //var result = (IDictionary<string, object>)fb.Get("/me");
                    //var name = (string)result["name"];
                }
                else
                {

                    MessageBox.Show(facebookOAuthResult.ErrorDescription);
                    MessageBox.Show(facebookOAuthResult.ErrorReason);
                }
            }
        }
    }
}
