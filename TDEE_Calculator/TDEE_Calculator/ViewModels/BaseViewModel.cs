﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TDEE_Calculator.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public BaseViewModel()
        {
        }
    }
}
