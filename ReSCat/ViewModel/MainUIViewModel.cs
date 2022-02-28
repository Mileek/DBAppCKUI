using ReSCat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReSCat.ViewModel
{
    internal class MainUIViewModel : ObservableObject
    {
        private Visibility _showCalendar = Visibility.Hidden;
        public Visibility ShowCalendar
        {
            get
            {
                return _showCalendar;
            }
            set
            {
                _showCalendar = value;
                OnPropertyChanged();

            }
        }

        

        private string _maximize = "\uD83D\uDDD6";

        public string Maximize
        {
            get
            {
                return _maximize;
            }
            set
            {   
                _maximize = value;
                OnPropertyChanged();
            }
        }

    }
}
