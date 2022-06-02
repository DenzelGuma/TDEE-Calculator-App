using System;
using TDEE_Calculator.Services;
using TDEE_Calculator.Navigation;
using TDEE_Calculator.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TDEE_Calculator.Interfaces;

namespace TDEE_Calculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            IocProvider.Init();
            MainPage = new NavigationPage(new MainPage());
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
