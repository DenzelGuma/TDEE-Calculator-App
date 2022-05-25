using System;

using Xamarin.Forms;

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

        public TDEECalculatorPageViewModel()
        {
            CalculateTDEECommand = new Command(CalculateTDEE, () =>
            {
                return Gender != null && Age != 0 && Weight != 0 && Height != 0; 
            }
            );
        }
    }
}

