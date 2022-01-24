using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;
using ClosedXML.Excel;
using Microsoft.Win32;

namespace DBAppCK
{
    /// <summary>
    /// Logika interakcji dla klasy WPFDBAppCK.xaml
    /// </summary>

    public partial class WPFDBAppCK : Window
    {
        public List<MainTable> newSourceFile = new List<MainTable>(); //Public variable that store list from external excel file
        public WPFDBAppCK()
        {
            InitializeComponent();            
        }


        private void RunDB_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities entities = new MainDataBaseEntities())
            {
                //var elements = from el in entities.MainTables select el;

                if (newSourceFile.Count == 0)
                {
                    this.MainDatabaseXAML.ItemsSource = entities.MainTables.ToList(); //Item source of datagrid is main table, without any filtering                    
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to switch to main database?, " +
                                       "all progress will be deleted !", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //After pressing No do nothing
                    }
                    else
                    {
                        newSourceFile.Clear(); //Clearing whole variable(List)

                        this.MainDatabaseXAML.ItemsSource = entities.MainTables.ToList();
                    }
                }
                

            }




        }
        private void AddNewElementToGrid_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities entities = new MainDataBaseEntities())
            {
                if (Convert.ToInt32(TextElementPlannedWeek.Text) <= 0
                || Convert.ToInt32(TextElementActualWeek.Text) <= 0
                || Convert.ToDouble(TextElementWeight.Text) <= 0
                || Convert.ToInt32(TextElementQuantity.Text) <= 0)
                {
                    TextElementPlannedWeek.Text = string.Empty;
                    TextElementActualWeek.Text = string.Empty;
                    TextElementWeight.Text = string.Empty;
                    TextElementQuantity.Text = string.Empty;
                    MessageBox.Show("Planned week, Actual Week, Weight and Quantity can not be negative!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
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
                        if (newSourceFile.Count == 0) //Dependency wheter open was clicked or not
                        {
                            entities.MainTables.Add(mainTableAdd);
                            entities.SaveChanges();
                        }
                        else
                        {
                            newSourceFile.Add(mainTableAdd);
                        }
                    }
                    catch (Exception)
                    {
                        TextElementPlannedWeek.Text = string.Empty;
                        TextElementActualWeek.Text = string.Empty;
                        TextElementWeight.Text = string.Empty;
                        TextElementQuantity.Text = string.Empty;
                        MessageBox.Show("Planned week, Actual Week, Weight and Quantity must be a number !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                //Displaying
                if (newSourceFile.Count == 0)
                {
                    //Work on base database in entity
                    var elements = from el in entities.MainTables select el; //fcn to show all elements in datagrid
                    MainDatabaseXAML.ItemsSource = elements.ToList(); // displaying everything as list
                }
                else
                {
                    //Add entry to excel file that was opened
                    MainDatabaseXAML.ItemsSource = newSourceFile;
                    MainDatabaseXAML.Items.Refresh();
                }
            }

        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities entities = new MainDataBaseEntities())
            {

                if (newSourceFile.Count == 0)
                {
                    //Working on MainDataBase - Entity

                    MainTable mainTableDel = (MainTable)MainDatabaseXAML.SelectedItem;
                    entities.MainTables.Attach(mainTableDel);
                    entities.MainTables.Remove(mainTableDel);
                    entities.SaveChanges();

                    //Displaying
                    var elements = from el in entities.MainTables select el; //fcn to show all elements in datagrid
                    MainDatabaseXAML.ItemsSource = elements.ToList(); // displaying everything as list 
                }
                else
                {
                    //Working on loaded file
                    newSourceFile.RemoveAt(MainDatabaseXAML.SelectedIndex);
                    MainDatabaseXAML.ItemsSource = newSourceFile;
                    MainDatabaseXAML.Items.Refresh();
                }

            }

        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities entities = new MainDataBaseEntities())
            {
                try
                {
                    MainTable mainTableUpdate = (MainTable)MainDatabaseXAML.SelectedItem;

                    if (newSourceFile.Count == 0)
                    {

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
                    else
                    {
                        //mainTableUpdate is storing items that are replaced
                        mainTableUpdate.Planned_Week = Convert.ToInt32(TextElementPlannedWeek.Text);
                        mainTableUpdate.Actual_Week = Convert.ToInt32(TextElementActualWeek.Text);
                        mainTableUpdate.Weight = Convert.ToDouble(TextElementWeight.Text);
                        mainTableUpdate.Order = TextElementOrder.Text;
                        mainTableUpdate.Client_Name = TextElementClientName.Text;
                        mainTableUpdate.Name = TextElementName.Text;
                        mainTableUpdate.Quantity = Convert.ToInt32(TextElementQuantity.Text);

                        //newSourceFile values are replaced by mainTableUpdate that is stroing replaced elements
                        //(new System.Collections.Generic.Mscorlib_CollectionDebugView<DBAppCK.MainTable>(newSourceFile).Items[0]).Client_Name <- !!Debugging!!
                        newSourceFile[MainDatabaseXAML.SelectedIndex].Planned_Week = mainTableUpdate.Planned_Week;
                        newSourceFile[MainDatabaseXAML.SelectedIndex].Actual_Week = mainTableUpdate.Actual_Week;
                        newSourceFile[MainDatabaseXAML.SelectedIndex].Weight = mainTableUpdate.Weight;
                        newSourceFile[MainDatabaseXAML.SelectedIndex].Order = mainTableUpdate.Order;
                        newSourceFile[MainDatabaseXAML.SelectedIndex].Client_Name = mainTableUpdate.Client_Name;
                        newSourceFile[MainDatabaseXAML.SelectedIndex].Name = mainTableUpdate.Name;
                        newSourceFile[MainDatabaseXAML.SelectedIndex].Quantity = mainTableUpdate.Quantity;

                        //Clearing text boxes
                        TextElementPlannedWeek.Text = string.Empty;
                        TextElementActualWeek.Text = string.Empty;
                        TextElementWeight.Text = string.Empty;
                        TextElementOrder.Text = string.Empty;
                        TextElementClientName.Text = string.Empty;
                        TextElementName.Text = string.Empty;
                        TextElementQuantity.Text = string.Empty;

                        //Displaying source and refreshing so that values are visable
                        MainDatabaseXAML.ItemsSource = newSourceFile;
                        MainDatabaseXAML.Items.Refresh();

                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("You must select row to update values !");
                }
            }

        }

        private void MainDatabaseXAML_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (MainDataBaseEntities entities = new MainDataBaseEntities())
            {
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
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

            using (MainDataBaseEntities entities = new MainDataBaseEntities())
            {

                var SelectedItem = (ComboBoxItem)searchList.SelectedValue;
                var content = ((TextBlock)SelectedItem.Content).Text; //Debugging window shows exact variable

                if (newSourceFile.Count == 0)
                {
                    if (content == "Planned Week")
                    {
                        var elements = from el in entities.MainTables.ToList() where (Convert.ToString(el.Planned_Week).Contains(SearchItems.Text)) select el;
                        MainDatabaseXAML.ItemsSource = elements.ToList();
                    }
                    else if (content == "Actual Week")
                    {
                        var elements = from el in entities.MainTables.ToList() where (Convert.ToString(el.Actual_Week).Contains(SearchItems.Text)) select el;
                        MainDatabaseXAML.ItemsSource = elements.ToList();
                    }
                    else if (content == "Weight")
                    {
                        var elements = from el in entities.MainTables.ToList() where (Convert.ToString(el.Weight).Contains(SearchItems.Text)) select el;
                        MainDatabaseXAML.ItemsSource = elements.ToList();
                    }
                    else if (content == "Order")
                    {
                        var elements = from el in entities.MainTables where (el.Order.Contains(SearchItems.Text)) select el;
                        MainDatabaseXAML.ItemsSource = elements.ToList();
                    }
                    else if (content == "Client Name")
                    {
                        var elements = from el in entities.MainTables where (el.Client_Name.Contains(SearchItems.Text)) select el;
                        MainDatabaseXAML.ItemsSource = elements.ToList();
                    }
                    else if (content == "Name")
                    {
                        var elements = from el in entities.MainTables where (el.Name.Contains(SearchItems.Text)) select el;
                        MainDatabaseXAML.ItemsSource = elements.ToList();
                    }
                    else if (content == "Quantity")
                    {
                        var elements = from el in entities.MainTables.ToList() where (Convert.ToString(el.Quantity).Contains(SearchItems.Text)) select el;
                        MainDatabaseXAML.ItemsSource = elements.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Something gone wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (content == "Planned Week")
                    {
                        var filter = newSourceFile.Where(newSourceFile => (Convert.ToString(newSourceFile.Planned_Week).Contains(SearchItems.Text)));
                        MainDatabaseXAML.ItemsSource = filter.ToList();
                        MainDatabaseXAML.Items.Refresh();
                    }
                    else if (content == "Actual Week")
                    {
                        var filter = newSourceFile.Where(newSourceFile => (Convert.ToString(newSourceFile.Actual_Week).Contains(SearchItems.Text)));
                        MainDatabaseXAML.ItemsSource = filter.ToList();
                        MainDatabaseXAML.Items.Refresh();
                    }
                    else if (content == "Weight")
                    {
                        var filter = newSourceFile.Where(newSourceFile => (Convert.ToString(newSourceFile.Weight).Contains(SearchItems.Text)));
                        MainDatabaseXAML.ItemsSource = filter.ToList();
                        MainDatabaseXAML.Items.Refresh();
                    }
                    else if (content == "Order")
                    {
                        var filter = newSourceFile.Where(newSourceFile => (newSourceFile.Order).Contains(SearchItems.Text));
                        MainDatabaseXAML.ItemsSource = filter.ToList();
                        MainDatabaseXAML.Items.Refresh();
                    }
                    else if (content == "Client Name")
                    {
                        var filter = newSourceFile.Where(newSourceFile => (newSourceFile.Client_Name).Contains(SearchItems.Text));
                        MainDatabaseXAML.ItemsSource = filter.ToList();
                        MainDatabaseXAML.Items.Refresh();
                    }
                    else if (content == "Name")
                    {
                        var filter = newSourceFile.Where(newSourceFile => (newSourceFile.Name).Contains(SearchItems.Text));
                        MainDatabaseXAML.ItemsSource = filter.ToList();
                        MainDatabaseXAML.Items.Refresh();
                    }
                    else if (content == "Quantity")
                    {
                        var filter = newSourceFile.Where(newSourceFile => (Convert.ToString(newSourceFile.Quantity).Contains(SearchItems.Text)));
                        MainDatabaseXAML.ItemsSource = filter.ToList();
                        MainDatabaseXAML.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Something gone wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
        }

        private void searchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedItem = (ComboBoxItem)searchList.SelectedValue; //Casting SearchList because content is too deep
            var content = ((TextBlock)SelectedItem.Content).Text; //Debugging window shows exact variable

            MessageBox.Show("You will search database by: " + content, "Search", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //After pressing No, currently no idea
            }
            else
            {
                Application.Current.Shutdown(); //Close app, saving is automatic so no msg needed?
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //In development



        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel|*.xlsx";
            saveFileDialog.ShowDialog(); //Display File dialog with possible file format (Excel only)
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {

                    MainDataBaseEntities entities = new MainDataBaseEntities();

                    if (newSourceFile.Count == 0)
                    {
                        var source = entities.MainTables.ToList(); // List from entity

                        //Inicialization
                        workbook.AddWorksheet("Table");
                        var wsS = workbook.Worksheet("Table");
                        var range = wsS.Cell("A2").InsertTable(source, true);
                        wsS.Columns().AdjustToContents();


                        //Cell/View manipulation HEADER
                        wsS.Cells("A1").Value = "Production Plan - Report based on Main DataBase";
                        wsS.Range("A1:H1").Merge();
                        wsS.Range("A1:H1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wsS.Range("A1:H1").Style.Fill.SetBackgroundColor(XLColor.BlueGray);
                        wsS.Range("A1:H1").Style.Font.FontSize = 16;
                        wsS.Range("A1:H1").Style.Font.Bold = true;
                        wsS.Range("A1:H1").Style.Font.FontColor = XLColor.Blue;

                        //Layout manipulation - in development
                        wsS.Row(2).Style.Fill.SetBackgroundColor(XLColor.DarkGray);

                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                    else
                    {
                        //Inicialization
                        workbook.AddWorksheet("Table");
                        var wsS = workbook.Worksheet("Table");
                        var range = wsS.Cell("A2").InsertTable(newSourceFile, true);
                        wsS.Columns().AdjustToContents();

                        //Cell/View manipulation HEADER
                        wsS.Cells("A1").Value = "Production Plan - Report based on Main DataBase";
                        wsS.Range("A1:H1").Merge();
                        wsS.Range("A1:H1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wsS.Range("A1:H1").Style.Fill.SetBackgroundColor(XLColor.BlueGray);
                        wsS.Range("A1:H1").Style.Font.FontSize = 16;
                        wsS.Range("A1:H1").Style.Font.Bold = true;
                        wsS.Range("A1:H1").Style.Font.FontColor = XLColor.Blue;

                        //Layout manipulation - in development
                        wsS.Row(2).Style.Fill.SetBackgroundColor(XLColor.DarkGray);

                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel|*.xlsx";
            openFileDialog.ShowDialog();
            try
            {
                using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                {
                    MainDataBaseEntities entities = new MainDataBaseEntities();
                    List<MainTable> openList = new List<MainTable>();


                    var wsO = workbook.Worksheet(1); //Use 1 sheet in chosen pathfile

                    int rowStart = 3; //Skip header and names
                    int columnStart = 1;



                    while (string.IsNullOrWhiteSpace(wsO.Cell(rowStart, columnStart).Value?.ToString()) == false) // Is thre data in row? ID number
                    {
                        MainTable tableOpen = new MainTable();

                        tableOpen.Id = int.Parse(wsO.Cell(rowStart, columnStart).Value.ToString()); // change value from double to string, and then to int
                        tableOpen.Planned_Week = int.Parse(wsO.Cell(rowStart, columnStart + 1).Value.ToString());
                        tableOpen.Actual_Week = int.Parse(wsO.Cell(rowStart, columnStart + 2).Value.ToString());
                        tableOpen.Weight = double.Parse(wsO.Cell(rowStart, columnStart + 3).Value.ToString());
                        tableOpen.Order = wsO.Cell(rowStart, columnStart + 4).Value.ToString();
                        tableOpen.Client_Name = wsO.Cell(rowStart, columnStart + 5).Value.ToString();
                        tableOpen.Name = wsO.Cell(rowStart, columnStart + 6).Value.ToString();
                        tableOpen.Quantity = int.Parse(wsO.Cell(rowStart, columnStart + 7).Value.ToString());

                        openList.Add(tableOpen);
                        rowStart++;
                    }

                    MainDatabaseXAML.ItemsSource = openList;
                    newSourceFile = openList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainDatabaseXAML_CurrentCellChanged(object sender, EventArgs e)
        {
            
        }
    }
}
