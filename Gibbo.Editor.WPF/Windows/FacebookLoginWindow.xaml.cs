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
using System.Dynamic;
using System.Windows;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for FirstLoginWindow.xaml
    /// </summary>
    public partial class FacebookLoginWindow : Window
    {
        #region fields
        private readonly Uri _loginUrl;
        protected readonly FacebookClient _fb;
        #endregion


        #region Properties
        public FacebookOAuthResult FacebookOAuthResult { get; private set; }
        #endregion

        public FacebookLoginWindow(string appId, string extendedPermissions)
            : this(new FacebookClient(), appId, extendedPermissions)
        {
        }

        public FacebookLoginWindow(FacebookClient fb, string appId, string extendedPermissions)
        {
            if (fb == null)
                throw new ArgumentNullException("fb");
            if (string.IsNullOrWhiteSpace(appId))
                throw new ArgumentNullException("appId");

            _fb = fb;
            _loginUrl = GenerateLoginUrl(appId, extendedPermissions);

            InitializeComponent();
        }

        #region Methods
        private Uri GenerateLoginUrl(string appId, string extendedPermissions)
        {
            // for .net 3.5
            // var parameters = new Dictionary<string,object>
            // parameters["client_id"] = appId;
            dynamic parameters = new ExpandoObject();
            parameters.client_id = appId;
            parameters.redirect_uri = "https://www.facebook.com/connect/login_success.html";

            // The requested response: an access token (token), an authorization code (code), or both (code token).
            parameters.response_type = "token";

            // list of additional display modes can be found at http://developers.facebook.com/docs/reference/dialogs/#display
            parameters.display = "popup";

            // add the 'scope' parameter only if we have extendedPermissions.
            if (!string.IsNullOrWhiteSpace(extendedPermissions))
                parameters.scope = extendedPermissions;

            // when the window is loaded navigate to the login url.
            return _fb.GetLoginUrl(parameters);
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // make sure to use AbsoluteUri
            webBrowser.Navigate(_loginUrl.AbsoluteUri);
        }

        private void webBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // whenever the browser navigates to a new url, try parsing the url.
            // the url may be the result of OAuth 2.0 authentication.

            FacebookOAuthResult oauthResult;
            if (_fb.TryParseOAuthCallbackUrl(e.Uri, out oauthResult))
            {
                // The url is the result of OAuth 2.0 authentication
                FacebookOAuthResult = oauthResult;

                this.DialogResult = FacebookOAuthResult.IsSuccess ? true : false;
            }
            else
            {
                // The url is NOT the result of OAuth 2.0 authentication.
                FacebookOAuthResult = null;
            }
        }


    }
}
