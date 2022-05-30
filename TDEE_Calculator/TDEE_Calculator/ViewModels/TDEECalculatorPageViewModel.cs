using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using TDEE_Calculator.Navigation;
using TDEE_Calculator.Interfaces;
using TDEE_Calculator.Models;
using TDEE_Calculator.Views;
using System.ComponentModel;

namespace TDEE_Calculator.ViewModels
{
    public class TDEECalculatorPageViewModel : BaseViewModel
    {
        private string _gender;
        private int _age;
        private double _height;
        private double _weight;
        private int _activityPW;
        private int _tdee;

        public ObservableCollection<Tdee_stats> Stats { get; set; } = new ObservableCollection<Tdee_stats>();
        private readonly IDbService _dbService;

        public string Gender
        {
            get => _gender;
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    CalculateTDEECommand.ChangeCanExecute();
                    OnPropertyChanged(nameof(Gender));
                }
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    CalculateTDEECommand.ChangeCanExecute();
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    CalculateTDEECommand.ChangeCanExecute();
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        public double Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    CalculateTDEECommand.ChangeCanExecute();
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }

        public int ActivityPW
        {
            get => _activityPW;
            set
            {
                if (_activityPW != value)
                {
                    _activityPW = value;
                    CalculateTDEECommand.ChangeCanExecute();
                    OnPropertyChanged(nameof(ActivityPW));
                }
            }
        }

        public int Tdee
        {
            get => _tdee;
            set
            {
                if (_tdee != value)
                {
                    _tdee = value;
                    OnPropertyChanged(nameof(Tdee));
                }
            }
        }

        public Command CalculateTDEECommand { get; set; }

        public ICommand AddToDatabaseCommand { get; set; }

        

        public void CalculateTDEE()
        {
            var Fmbr = 655 + (9.6 * Weight) +(1.8 * Height) - (4.7 * Age); // Female BMR Harris-Benedict Equation
            var Mmbr = 66 + (13.7 * Weight) +(5 * Height) - (6.8 * Age);   // Male BMR Harris-Benedict Equation

            if (Gender == "F" || Gender == "f")
            {
                if (ActivityPW == 0)
                {
                    Tdee = (int)(Fmbr * 1.2);
                }

                if (ActivityPW >= 1 && ActivityPW <=2)
                {
                    Tdee = (int)(Fmbr * 1.375);
                }

                if (ActivityPW >= 3 && ActivityPW <= 5)
                {
                    Tdee = (int)(Fmbr * 1.55);
                }

                if (ActivityPW >= 6 && ActivityPW <= 7)
                {
                    Tdee = (int)(Fmbr * 1.725);
                }

                if (ActivityPW >8)
                {
                    Tdee = (int)(Fmbr * 1.9);
                }

            }

            if (Gender == "M" || Gender == "m")
            {
                if (ActivityPW == 0)
                {
                    Tdee = (int)(Mmbr * 1.2);
                }

                if (ActivityPW >= 1 && ActivityPW <= 2)
                {
                    Tdee = (int)(Mmbr * 1.375);
                }

                if (ActivityPW >= 3 && ActivityPW <= 5)
                {
                    Tdee = (int)(Mmbr * 1.55);
                }

                if (ActivityPW >= 6 && ActivityPW <= 7)
                {
                    Tdee = (int)(Mmbr * 1.725);
                }

                if (ActivityPW > 8)
                {
                    Tdee = (int)(Mmbr * 1.9);
                }
            }

        }


        public TDEECalculatorPageViewModel(IDbService dbService)
        {
            _dbService = dbService;
            CalculateTDEECommand = new Command(CalculateTDEE, () =>
            {
                return Gender != null && Age != 0 && Weight != 0 && Height != 0;
                
            }
            );

            
            AddToDatabaseCommand = new Command(async () => await AddToDatabase(Gender,Age,Height,Weight,ActivityPW,Tdee));
            
        }

      

   
        private async Task AddToDatabase(string gender,int age,double height, double weight , int activtyPW, int tdee)
        {
            var newTdee_stats = new Tdee_stats
            {
                Gender = gender,
                Age = age,
                Height = height,
                Weight = weight,
                ActivtyPW = activtyPW,
                Tdee = tdee,

            };

            var addToDb = await _dbService.SaveStatsAsync(newTdee_stats);

            if (addToDb > 0)
            {
                Stats.Add(newTdee_stats);
            }

           
            Gender = string.Empty;
            Age = 0;
            Height = 0;
            Weight = 0;
        }

        public override async Task Initialise()
        {

            var currentStats = await _dbService.GetStatsAsync();

            if (currentStats.Count > 0)
            {
                currentStats.ForEach(stat => Stats.Add(stat));
            }
            else
            {
                Stats = new ObservableCollection<Tdee_stats>();
            }
        }

    }
}

