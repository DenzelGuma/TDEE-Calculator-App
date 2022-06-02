using Microsoft.Extensions.DependencyInjection;
using TDEE_Calculator.Interfaces;
using TDEE_Calculator.Models;
using TDEE_Calculator.Services;
using TDEE_Calculator.ViewModels;
using Xamarin.Forms;

namespace TDEE_Calculator.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = IocProvider.ServiceProvider.GetService<MainPageViewModel>();
        }

     
    }
}
