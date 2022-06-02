using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using AppTests.Services;
using Autofac;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;

using TDEE_Calculator.Interfaces;
using TDEE_Calculator.Models;
using TDEE_Calculator.ViewModels;

namespace AppTests.Viewmodels
{
    public class TDEECalculatorPageViewModelsTests
    {
        [Test]
        public async Task Initialise_Should_Pass_When_Service_Returns_Users_From_Db()
        {
            //Arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IDbService>().Setup(method => method.GetStatsAsync()).ReturnsAsync(new List<Tdee_stats> {
                new Tdee_stats {
                    Gender="M",
                    Age=23,
                    Height = 180,
                    Weight = 90,
                    ActivtyPW = 0,
                    ID=1
                },
            });

            var viewmodelToTest = mock.Create<TDEECalculatorPageViewModel>();

            //Act
            await viewmodelToTest.Initialise();

            //Assert
            Assert.IsTrue(viewmodelToTest.Stats.Count == 1);
            Assert.IsTrue(viewmodelToTest.Stats[0].ID == 1);
        }

        [Test]
        public async Task AddToDatabase_Should_Pass_When_User_Entered_Is_Valid()
        {
            //Arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IDbService>().Setup(method => method.GetStatsAsync()).ReturnsAsync(new List<Tdee_stats> {
                new Tdee_stats {
                    Gender="M",
                    Age=23,
                    Height = 180,
                    Weight = 90,
                    ActivtyPW = 0,
                    ID=1
                },
            });

            mock.Mock<IDbService>().Setup(method => method.SaveStatsAsync(It.IsAny<Tdee_stats>())).ReturnsAsync(1);

            var viewmodelToTest = mock.Create<TDEECalculatorPageViewModel>();
            viewmodelToTest.Gender = "F";
            viewmodelToTest.Age = 46;
            viewmodelToTest.Height = 160;
            viewmodelToTest.Weight = 70;
            viewmodelToTest.ActivityPW = 10;

            //Act
            await viewmodelToTest.Initialise();
            viewmodelToTest.AddToDatabaseCommand.Execute(null);

            //Assert
            Assert.IsTrue(viewmodelToTest.Stats.Count == 2);
            Assert.IsTrue(viewmodelToTest.Stats[1].Gender == "F");
            Assert.IsTrue(viewmodelToTest.Stats[1].Age == 46);
            Assert.IsTrue(viewmodelToTest.Stats[1].Height == 160);
            Assert.IsTrue(viewmodelToTest.Stats[1].Weight == 70);
            Assert.IsTrue(viewmodelToTest.Stats[1].ActivtyPW == 10);
      
        }

        [Test]
        public async Task AddToDatabase_Should_Fail_When_User_Entered_Is_InValid()
        {
            //Arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IDbService>().Setup(method => method.GetStatsAsync()).ReturnsAsync(new List<Tdee_stats>
            {
                new Tdee_stats {
                    Gender="M",
                    Age=23,
                    Height = 180,
                    Weight = 90,
                    ActivtyPW = 0,
                    ID=1
                },
               new Tdee_stats {
                    Gender="F",
                    Age=24,
                    Height = 190,
                    Weight = 100,
                    ActivtyPW = 1,
                    ID=2
                },
               new Tdee_stats {
                    Gender="m",
                    Age=25,
                    Height = 200,
                    Weight = 110,
                    ActivtyPW = 2,
                    ID=3
                },
              new Tdee_stats {
                    Gender="f",
                    Age=26,
                    Height = 210,
                    Weight = 120,
                    ActivtyPW = 4,
                    ID=4
                }
            });
            mock.Mock<IDbService>().Setup(method => method.SaveStatsAsync(It.IsAny<Tdee_stats>())).ReturnsAsync(0);

            var viewmodelToTest = mock.Create<TDEECalculatorPageViewModel>();
            viewmodelToTest.Gender = "f";
            viewmodelToTest.Age = 26;
            viewmodelToTest.Height = 210;
            viewmodelToTest.Weight = 120;
            viewmodelToTest.ActivityPW = 4;

            //Act
            await viewmodelToTest.Initialise();
            viewmodelToTest.AddToDatabaseCommand.Execute(null);

            //Assert
            Assert.IsTrue(viewmodelToTest.Stats.Count == 4);

        }
    }

}
