using ReSCat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReSCat.ViewModel
{
    public class DataGridViewModel : ObservableObject
    {
        private Visibility showDataBaseMenu = Visibility.Hidden;
        public Visibility ShowDataBaseMenu
        {
            get
            {
                return showDataBaseMenu;
            }
            set
            {
                showDataBaseMenu = value;

                //OnPropertyChanged("ShowDataBaseMenu");
            }
        }
    }
}
