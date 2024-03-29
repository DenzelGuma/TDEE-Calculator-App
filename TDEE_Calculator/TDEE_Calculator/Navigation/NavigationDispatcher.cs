﻿using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace TDEE_Calculator.Navigation
{
    public class NavigationDispatcher
    {
        private static NavigationDispatcher _instance;

        private INavigation _navigation;

        public static NavigationDispatcher Instance =>
                      _instance ?? (_instance = new NavigationDispatcher());

        public INavigation Navigation =>
                     _navigation ?? throw new Exception("NavigationDispatcher is not initialized");

        public static NavigationDispatcher GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NavigationDispatcher();
            }
            return _instance;
        }

        public void Initialize(INavigation navigation)
        {
            _navigation = navigation;
        }
    }
}

