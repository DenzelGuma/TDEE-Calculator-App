using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TDEE_Calculator.Models;
using TDEE_Calculator.ViewModels;
using Xamarin.Forms;

namespace TDEE_Calculator.Views
{
    public partial class TDEECalculatorPage : ContentPage
    {
        public TDEECalculatorPage()
        {
            InitializeComponent();
            BindingContext = IocProvider.ServiceProvider.GetService<TDEECalculatorPageViewModel>();
            SubscribeToEvents();

        }

        private void SubscribeToEvents()
        {
            Appearing += TDEECalculatorPage_Appearing;
        }

        private async void TDEECalculatorPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                await (BindingContext as TDEECalculatorPageViewModel).Initialise();
            }
            catch (Exception error)
            {
                Debug.Fail(error.Message); //handle gracefully here
            }
        }


    }
}