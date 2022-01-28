using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DBAppCK.ViewModel
{
    public class VisibilityCalendarViewModel : INotifyPropertyChanged
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

                NotifyPropertyChanged("ShowCalendar");
            }
        }
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;                
    }
    
}
