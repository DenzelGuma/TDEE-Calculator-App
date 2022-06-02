using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Refit;
using TDEE_Calculator.Navigation;
using TDEE_Calculator.Interfaces;
using TDEE_Calculator.Models;
using TDEE_Calculator.Services;
using TDEE_Calculator.Views;
using Xamarin.Forms;

namespace TDEE_Calculator.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        readonly IGoogleClientManager _googleService = CrossGoogleClient.Current;
        public ICommand OnLoginCommand { get; set; }

        IOAuth2Service _oAuth2Service;

        // Selected Social Media platform (Google in this case)
        public ObservableCollection<AuthNetwork> AuthenticationNetworks { get; set; } = new ObservableCollection<AuthNetwork>()
        {
              new AuthNetwork()
            {
                Name = "Google",
                Icon = "ic_google",
                Foreground = "#000000",
                Background ="#F8F8F8"
            }
        };


        public MainPageViewModel()
        {
            OnLoginCommand = new Command<AuthNetwork>(async (data) => await LoginGoogleAsync(data));
        }
        
        //Google Api Login call
        public async Task LoginGoogleAsync(AuthNetwork authNetwork)
        {
            try
            {
                if (!string.IsNullOrEmpty(_googleService.AccessToken))
                {
                    //Always require user authentication
                    _googleService.Logout();
                }

                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        //Successful login 
                        case GoogleActionStatus.Completed:
#if DEBUG
                            var googleUserString = JsonConvert.SerializeObject(e.Data);
                            Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
#endif

                            var socialLoginData = new NetworkAuthData
                            {
                                Id = e.Data.Id,
                                Logo = authNetwork.Icon,
                                Foreground = authNetwork.Foreground,
                                Background = authNetwork.Background,
                                Picture = e.Data.Picture.AbsoluteUri,
                                Name = e.Data.Name,
                            };
                            
                            await App.Current.MainPage.Navigation.PushModalAsync(new TDEECalculatorPage());
                            break;
                        case GoogleActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                            break;
                        case GoogleActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

                await _googleService.LoginAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public override Task Initialise()
        {
            throw new NotImplementedException();
        }

    }
}