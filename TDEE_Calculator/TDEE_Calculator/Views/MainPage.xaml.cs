using TDEE_Calculator.Models;
using TDEE_Calculator.Services;
using TDEE_Calculator.ViewModels;
using Xamarin.Forms;

namespace TDEE_Calculator.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(IOAuth2Service oAuth2Service)
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel(oAuth2Service);
        }

     
    }
}
