using System;
using System.Collections.Generic;
using System.Text;
using TDEE_Calculator;
using Microsoft.Extensions.DependencyInjection;

namespace TDEE_Calculator
{
    public class IocProvider
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceProvider Init()
        {
            ServiceProvider serviceProvider;

            serviceProvider = new ServiceCollection()
            .ConfigureServices()
            .ConfigureViewModels()
            .BuildServiceProvider();

            ServiceProvider = serviceProvider;

            return serviceProvider;
        }
    }
}

