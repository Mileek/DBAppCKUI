using ReSCat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReSCat.ViewModel
{
    public class MaximizeViewModel : ObservableObject
    {
        private string _maximize = "something";

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
