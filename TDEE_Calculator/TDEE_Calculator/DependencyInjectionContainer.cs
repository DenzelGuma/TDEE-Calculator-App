
using System;

//using TDEE_Calculator.Interfaces;
//using TDEE_Calculator.Services;
using TDEE_Calculator.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace TDEE_Calculator
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //services.AddSingleton<xService, yService>();

            return services;
        }

        public static IServiceCollection ConfigureMockServices(this IServiceCollection services)
        {
            //Mocks to be added

            return services;
        }

        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<TDEECalculatorPageViewModel>();


            return services;
        }
    }
}
