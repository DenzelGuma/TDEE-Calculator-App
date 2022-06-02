using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Auth;

using TDEE_Calculator.Interfaces;
using TDEE_Calculator.Models;


namespace TDEE_Calculator.Services
{
       
    public class OAuth2Service : IOAuth2Service , IServiceOverridable
    {
        private string clientId;
        private string scope;
        private Uri authorizeUrl;
        private Uri redirectUrl;

        public OAuth2Service(string clientId, string scope, Uri authorizeUrl, Uri redirectUrl)
        {
            this.clientId = clientId;
            this.scope = scope;
            this.authorizeUrl = authorizeUrl;
            this.redirectUrl = redirectUrl;
        }

        public bool AllowCancel { get; private set; }
        public bool ShowErrors { get; private set; }
        public EventHandler<AuthenticatorErrorEventArgs> Error { get; private set; }
        public EventHandler<AuthenticatorCompletedEventArgs> Completed { get; private set; }

        public event EventHandler<string> OnSuccess = delegate { };
        public event EventHandler OnCancel = delegate { };
        public event EventHandler<string> OnError = delegate { };

        public void Authenticate(string clientId, string scope, Uri authorizeUrl, Uri redirectUrl)
        {
            

            var auth = new OAuth2Service(
                clientId: clientId, // your OAuth2 client id
                scope: scope, // the scopes for the particular API you're accessing, delimited by "+" symbols
                authorizeUrl: authorizeUrl, // the auth URL for the service
                redirectUrl: redirectUrl); // the redirect URL for the service

            auth.AllowCancel = true;
            auth.ShowErrors = false;

            EventHandler<AuthenticatorErrorEventArgs> errorDelegate = null;
            EventHandler<AuthenticatorCompletedEventArgs> completedDelegate = null;

            errorDelegate = (sender, eventArgs) =>
            {
                OnError?.Invoke(this, eventArgs.Message);

                auth.Error -= errorDelegate;
                auth.Completed -= completedDelegate;
            };

            completedDelegate = (sender, eventArgs) => {


                if (eventArgs.IsAuthenticated)
                {

                    OnSuccess?.Invoke(this, eventArgs.Account.Properties["access_token"]);

                }
                else
                {
                    // The user cancelled

                    OnCancel?.Invoke(this, EventArgs.Empty);
                }
                auth.Error -= errorDelegate;
                auth.Completed -= completedDelegate;

            };

            auth.Error += errorDelegate;
            auth.Completed += completedDelegate;

           
        }

    }
}
