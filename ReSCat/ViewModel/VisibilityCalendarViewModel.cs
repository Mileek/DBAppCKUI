﻿using ReSCat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReSCat.ViewModel
{
    public class VisibilityCalendarViewModel : ObservableObject
    {
        private Visibility showCalendar = Visibility.Hidden;
        public Visibility ShowCalendar
        {
            get
            {
                return showCalendar;
            }
            set
            {   
                showCalendar = value;

                OnPropertyChanged();

            }
        }
    }

}
