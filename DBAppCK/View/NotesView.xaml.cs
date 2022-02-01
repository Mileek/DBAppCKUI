using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DBAppCK.View
{
    /// <summary>
    /// Logika interakcji dla klasy NotesView.xaml
    /// </summary>
    public partial class NotesView : Window
    {
        public NotesView()
        {
            InitializeComponent();
        }

        private void DragnMove(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

    }
}
