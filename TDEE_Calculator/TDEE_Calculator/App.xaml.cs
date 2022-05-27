using System;
using TDEE_Calculator.Services;
using TDEE_Calculator.Navigation;
using TDEE_Calculator.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDEE_Calculator
{
    public partial class App : Application
    {
        public App(IOAuth2Service oAuth2Service)
        {
            InitializeComponent();
            IocProvider.Init();
            MainPage = new NavigationPage(new MainPage(oAuth2Service));
            NavigationDispatcher.Instance.Initialize(MainPage.Navigation);
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
