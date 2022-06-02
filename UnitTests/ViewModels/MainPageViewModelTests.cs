using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using AppTests.Services;
using Autofac;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using Plugin.GoogleClient;
using TDEE_Calculator.Interfaces;
using TDEE_Calculator.Models;
using TDEE_Calculator.Services;
using TDEE_Calculator.ViewModels;

namespace AppTests.Viewmodels
{
    public class MainPageViewModelsTests
    {

        string clientId = "726991808032-4kipshpjefcnv2lm1cmfcdoh84hrrb4p.apps.googleusercontent.com";
        string scope = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email" ;
        Uri authorizeUrl = new Uri("https://tdeecalculatorclient.firebaseapp.com");
        Uri redirectUrl = new Uri("https://tdeecalculatorclient.firebaseapp.com/__/auth/handler");
        

        [Test]
        public async Task Login_Task_Ran_Successfuly()
        {
            //Arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IOAuth2Service>().Setup(method => method.Authenticate(clientId,scope, authorizeUrl, redirectUrl));
           
            var viewmodelToTest = mock.Create<MainPageViewModel>();

            //Act
            await viewmodelToTest.LoginGoogleAsync(new AuthNetwork());
            

            //Assert

            Assert.IsTrue(viewmodelToTest.LoginGoogleAsync(new AuthNetwork()).IsCompletedSuccessfully);
           
        }

        
    }

}
