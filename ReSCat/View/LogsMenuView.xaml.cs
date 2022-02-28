using Microsoft.Win32;
using ReSCat.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReSCat.View
{
    /// <summary>
    /// Logika interakcji dla klasy LogsView.xaml
    /// </summary>
    public partial class LogsMenuView : Page
    {
        public LogsMenuView()
        {
            InitializeComponent();
        }

        private void ClearLogs_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Dou you really want to clear the logs?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.No)
            {
                MessageBox.Show("Nothing happen");
            }
            else
            {
                LogsModel.ListLog.Clear();
                LogBlock.Text = String.Empty;
                MessageBox.Show("Logs were deleted succesfully!");
            }

        }

        private void SaveLogs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, LogBlock.Text);
            }
        }

        private void ShowLogs_Click(object sender, RoutedEventArgs e)
        {
            LogBlock.Inlines.Clear();
            var logRecords = LogsModel.ListLog;
            foreach (var item in logRecords)
            {
                //Inlines, so that the method will build string of my collection (List)
                LogBlock.Inlines.Add(item.Log + "Planned Week: " + item.Planned_Week.ToString()
                    + "/Actual Week: " + item.Actual_Week.ToString()
                    + "/Weight: " + item.Weight.ToString()
                    + "/Order: " + item.Order.ToString()
                    + "/Client Name: " + item.Client_Name.ToString()
                    + "/Element Name: " + item.Name.ToString()
                    + "/Hall: " + item.Hall.ToString()
                    + "/Quantity: " + item.Quantity.ToString() + "\n");
            }
        }
    }
}
