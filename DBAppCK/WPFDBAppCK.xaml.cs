using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

namespace DBAppCK
{
    /// <summary>
    /// Logika interakcji dla klasy WPFDBAppCK.xaml
    /// </summary>

    public partial class WPFDBAppCK : Window
    {


        //public ObservableCollection<MainTable> dataCollection { get; set; }
        //public ObservableCollection<MainTable> ObsColl = new ObservableCollection<MainTable>();
        public WPFDBAppCK()
        {



            //dataCollection = new ObservableCollection<MainTable>();
            InitializeComponent();



            //var elements = from el in entitiesss.MainTables select el;



            //MainDatabaseXAML.ItemsSource = ObsColl;

            //var _itemSourceList = new CollectionViewSource() { Source = ObsColl };



            //ICollectionView Itemlist = _itemSourceList.View;

            ////var yourCostumFilter = new Predicate<object>(item => ((MainTable)item).Name.Contains(SearchItems.Text));

            ////Itemlist.Filter = yourCostumFilter;

            //MainDatabaseXAML.ItemsSource = Itemlist;


        }


        private void RunDB_Click(object sender, RoutedEventArgs e)
        {
            MainDataBaseEntities entities = new MainDataBaseEntities();
            //var elements = from el in entities.MainTables select el;
            this.MainDatabaseXAML.ItemsSource = entities.MainTables.ToList(); //Item source of datagrid is main table, without any filtering
            //MainDatabaseXAML.ItemsSource = elements.ToList(); 

        }
        private void AddNewElementToGrid_Click(object sender, RoutedEventArgs e)
        {
            MainDataBaseEntities entities = new MainDataBaseEntities();
            MainTable mainTableAdd = new MainTable()
            {
                Planned_Week = Convert.ToInt32(TextElementPlannedWeek.Text),
                Actual_Week = Convert.ToInt32(TextElementActualWeek.Text),
                Weight = Convert.ToDouble(TextElementWeight.Text),
                Order = TextElementOrder.Text,
                Client_Name = TextElementClientName.Text,
                Name = TextElementName.Text,
                Quantity = Convert.ToInt32(TextElementQuantity.Text)
            };
            entities.MainTables.Add(mainTableAdd);
            entities.SaveChanges();

            //Displaying
            var elements = from el in entities.MainTables select el; //fcn to show all elements in datagrid
            MainDatabaseXAML.ItemsSource = elements.ToList(); // displaying everything as list
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            MainDataBaseEntities entities = new MainDataBaseEntities();
            MainTable mainTableDel = (MainTable)MainDatabaseXAML.SelectedItem;
            entities.MainTables.Attach(mainTableDel);
            entities.MainTables.Remove(mainTableDel);
            entities.SaveChanges();

            //Displaying
            var elements = from el in entities.MainTables select el; //fcn to show all elements in datagrid
            MainDatabaseXAML.ItemsSource = elements.ToList(); // displaying everything as list

        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainDataBaseEntities entities = new MainDataBaseEntities();
                MainTable mainTableUpdate = (MainTable)MainDatabaseXAML.SelectedItem;


                entities.MainTables.Attach(mainTableUpdate);

                mainTableUpdate.Planned_Week = Convert.ToInt32(TextElementPlannedWeek.Text);
                mainTableUpdate.Actual_Week = Convert.ToInt32(TextElementActualWeek.Text);
                mainTableUpdate.Weight = Convert.ToDouble(TextElementWeight.Text);
                mainTableUpdate.Order = TextElementOrder.Text;
                mainTableUpdate.Client_Name = TextElementClientName.Text;
                mainTableUpdate.Name = TextElementName.Text;
                mainTableUpdate.Quantity = Convert.ToInt32(TextElementQuantity.Text);

                entities.SaveChanges();

                TextElementPlannedWeek.Text = string.Empty;
                TextElementActualWeek.Text = string.Empty;
                TextElementWeight.Text = string.Empty;
                TextElementOrder.Text = string.Empty;
                TextElementClientName.Text = string.Empty;
                TextElementName.Text = string.Empty;
                TextElementQuantity.Text = string.Empty;

                //Displaying
                var elements = from el in entities.MainTables select el; //fcn to show all elements in datagrid
                MainDatabaseXAML.ItemsSource = elements.ToList(); // displaying everything as list

                entities.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("You must select row to update values !");
            }

        }

        private void MainDatabaseXAML_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDataBaseEntities entities = new MainDataBaseEntities();

            MainTable mainTableDisplayRow = (MainTable)MainDatabaseXAML.SelectedItem;
            if (mainTableDisplayRow != null)
            {
                entities.MainTables.Attach(mainTableDisplayRow);

                TextElementPlannedWeek.Text = Convert.ToString(mainTableDisplayRow.Planned_Week);
                TextElementActualWeek.Text = Convert.ToString(mainTableDisplayRow.Actual_Week);
                TextElementWeight.Text = Convert.ToString(mainTableDisplayRow.Weight);
                TextElementOrder.Text = mainTableDisplayRow.Order;
                TextElementClientName.Text = mainTableDisplayRow.Client_Name;
                TextElementName.Text = mainTableDisplayRow.Name;
                TextElementQuantity.Text = Convert.ToString(mainTableDisplayRow.Quantity);

            }



        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MainDataBaseEntities entities = new MainDataBaseEntities();
            var SelectedItem = (ComboBoxItem)searchList.SelectedValue;
            var content = ((TextBlock)SelectedItem.Content).Text; //Debugging window shows exact variable

            if (content == "Planned Week")
            {
                var elements = from el in entities.MainTables where (Convert.ToString(el.Planned_Week).Contains(SearchItems.Text)) select el;
                MainDatabaseXAML.ItemsSource = elements.ToList();
            }
            else if (content == "Actual Week")
            {

            }
            else if (content =="Weight")
            {

            }
            else if (content =="Order")
            {

            }
            else if (content == "Client Name")
            {

            }
            else if (content =="Name")
            {
                var elements = from el in entities.MainTables where (el.Name.Contains(SearchItems.Text)) select el;
                MainDatabaseXAML.ItemsSource = elements.ToList();
            }
            else if (content =="Quantity")
            {

            }            
            else
            {
                MessageBox.Show("Somethink gone wrong");
            }

            //var elements = from el in entities.MainTables where (el.Name.Contains(SearchItems.Text)) select el;
            //MainDatabaseXAML.ItemsSource = elements.ToList();



            
        }

        private void searchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedItem = (ComboBoxItem)searchList.SelectedValue;                    
            var content = ((TextBlock)SelectedItem.Content).Text; //Debugging window shows exact variable
            
            MessageBox.Show("You will search database by: " + content);
        }
    }
}
