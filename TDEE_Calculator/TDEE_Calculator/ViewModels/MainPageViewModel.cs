using System;
using System.ComponentModel;

using TDEE_Calculator.Navigation;
using TDEE_Calculator.Views;

using Xamarin.Forms;

namespace TDEE_Calculator.ViewModels
{
    public class MainPageViewModel:BaseViewModel
    {
        public Command NavigateToNextPageCommand { get; set; }

        public MainPageViewModel()
        {
            NavigateToNextPageCommand = new Command(NavigateToNextPage);
        }

        public async void NavigateToNextPage()
        {
            var newPage = new TDEECalculatorPage();
            await NavigationDispatcher.Instance.Navigation.PushAsync(newPage);
        }


    }
}
