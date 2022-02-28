using ReSCat.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReSCat.View
{
    /// <summary>
    /// Logika interakcji dla klasy ChartMenuView.xaml
    /// </summary>
    public partial class ChartMenuView : Page
    {

        public ChartMenuView()
        {
            InitializeComponent();
        }

        //Whole function needs to be replaced, but the amount of possible configurations is 4, so its possible to work with if's
        private void ShowCharts_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {
                int i = 0;
                int previousIterationPlannedWeek = 0;
                int previousIterationActualWeek = 0;
                double previousIterationWeight = 0;
                double previousIterationQuantity = 0;
                

                if ((bool)Planned_Week.IsChecked && (bool)Weight.IsChecked)
                {

                    //Elements that were in our entity
                    var elementsInMainDB = from el in mainScreenEntity.MainTables
                                           orderby el.Planned_Week
                                           select el;

                    var allDataInDB = elementsInMainDB.ToList();
                    //Axises
                    double[] AxisX = new double[allDataInDB.Count];
                    double[] AxisY = new double[allDataInDB.Count];

                    //Chart Logic, points
                    foreach (var element in elementsInMainDB)
                    {
                        if (element.Planned_Week == previousIterationPlannedWeek)
                        {
                            AxisX[i] = (double)element.Planned_Week;
                            AxisY[i] = (double)element.Weight + previousIterationWeight;
                        }
                        else
                        {
                            AxisX[i] = (double)element.Planned_Week;
                            AxisY[i] = (double)element.Weight;
                        }

                        previousIterationPlannedWeek = (int)element.Planned_Week;
                        previousIterationWeight = AxisY[i];

                        i++;
                    }
                    //Plotting
                    PlotWPF.Plot.Clear();
                    PlotWPF.Plot.AddScatter(AxisX, AxisY);
                    PlotWPF.Refresh();
                }
                else if ((bool)Planned_Week.IsChecked && (bool)Quantity.IsChecked)
                {
                    //Elements that were in our entity
                    var elementsInMainDB = from el in mainScreenEntity.MainTables
                                           orderby el.Planned_Week
                                           select el;

                    var allDataInDB = elementsInMainDB.ToList();
                    //Axises
                    double[] AxisX = new double[allDataInDB.Count];
                    double[] AxisY = new double[allDataInDB.Count];

                    //Chart Logic, points
                    foreach (var element in elementsInMainDB)
                    {
                        if (element.Planned_Week == previousIterationPlannedWeek)
                        {
                            AxisX[i] = (double)element.Planned_Week;
                            AxisY[i] = (double)element.Quantity + previousIterationQuantity;
                        }
                        else
                        {
                            AxisX[i] = (double)element.Planned_Week;
                            AxisY[i] = (double)element.Quantity;
                        }

                        previousIterationPlannedWeek = (int)element.Planned_Week;
                        previousIterationQuantity = AxisY[i];

                        i++;
                    }
                    //Plotting
                    PlotWPF.Plot.Clear();
                    PlotWPF.Plot.AddScatter(AxisX, AxisY);
                    PlotWPF.Refresh();
                }
                else if ((bool)Actual_Week.IsChecked && (bool)Weight.IsChecked)
                {

                    //Elements that were in our entity
                    var elementsInMainDB = from el in mainScreenEntity.MainTables
                                           orderby el.Actual_Week
                                           select el;

                    var allDataInDB = elementsInMainDB.ToList();
                    //Axises
                    double[] AxisX = new double[allDataInDB.Count];
                    double[] AxisY = new double[allDataInDB.Count];

                    //Chart Logic, points
                    foreach (var element in elementsInMainDB)
                    {
                        if (element.Actual_Week == previousIterationActualWeek)
                        {
                            AxisX[i] = (double)element.Actual_Week;
                            AxisY[i] = (double)element.Weight + previousIterationWeight;
                        }
                        else
                        {
                            AxisX[i] = (double)element.Actual_Week;
                            AxisY[i] = (double)element.Weight;
                        }

                        previousIterationActualWeek = (int)element.Actual_Week;
                        previousIterationWeight = AxisY[i];

                        i++;
                    }
                    //Plotting
                    PlotWPF.Plot.Clear();
                    PlotWPF.Plot.AddScatter(AxisX, AxisY);
                    PlotWPF.Refresh();
                }
                else if ((bool)Actual_Week.IsChecked && (bool)Quantity.IsChecked)
                {
                    
                    //Elements that were in our entity
                    var elementsInMainDB = from el in mainScreenEntity.MainTables
                                           orderby el.Actual_Week
                                           select el;

                    var allDataInDB = elementsInMainDB.ToList();
                    //Axises
                    double[] AxisX = new double[allDataInDB.Count];
                    double[] AxisY = new double[allDataInDB.Count];

                    //Chart Logic, points
                    foreach (var element in elementsInMainDB)
                    {
                        if (element.Actual_Week == previousIterationActualWeek)
                        {
                            AxisX[i] = (double)element.Actual_Week;
                            AxisY[i] = (double)element.Quantity + previousIterationQuantity;
                        }
                        else
                        {
                            AxisX[i] = (double)element.Actual_Week;
                            AxisY[i] = (double)element.Quantity;
                        }

                        previousIterationActualWeek = (int)element.Actual_Week;
                        previousIterationQuantity = AxisY[i];

                        i++;
                    }
                    //Plotting
                    PlotWPF.Plot.Clear();
                    PlotWPF.Plot.AddScatter(AxisX, AxisY);
                    PlotWPF.Refresh();
                }

            }
        }

        private void ClearCharts_Click(object sender, RoutedEventArgs e)
        {
            PlotWPF.Plot.Clear();
            PlotWPF.Refresh();
        }
    }
}
