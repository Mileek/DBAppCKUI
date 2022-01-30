using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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
using DBAppCK.Model;
using DBAppCK.ViewModel;
using Microsoft.Win32;

namespace DBAppCK
{
    /// <summary>
    /// Logika interakcji dla klasy WPFDBAppCK.xaml
    /// </summary>

    //
    //Unique ID don't need to be "Recycled" and the amount of unique records is so big that it is not necessary (to delete it).
    //

    public partial class WPFDBAppCK : Window
    {
        private List<MainTable> loadedExcelFile = new List<MainTable>(); //Public variable that store list from external excel file
        private bool calendarVisibilityFlag = false;
        private void showFinishedItemsOrAll()
        {
            MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities();
            if (loadedExcelFile.Count == 0)
            {
                if (FinishedOnly.IsChecked == true)
                {
                    var elementsInMainDB = from el in mainScreenEntity.MainTables
                                           where el.IsFinished.Value == true
                                           orderby el.Actual_Week ascending
                                           select el;
                    MainDatabaseXAML.ItemsSource = elementsInMainDB.ToList();
                }
                else
                {
                    var elementsInMainDB = from el in mainScreenEntity.MainTables
                                           orderby el.Actual_Week ascending
                                           select el;
                    MainDatabaseXAML.ItemsSource = elementsInMainDB.ToList();
                }

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
                    loadedExcelFile.Clear(); //Clearing whole variable(List)

                    if (FinishedOnly.IsChecked == true)
                    {
                        var elementsInMainDB = from el in mainScreenEntity.MainTables
                                               where el.IsFinished.Value == true
                                               orderby el.Actual_Week ascending
                                               select el;
                        MainDatabaseXAML.ItemsSource = elementsInMainDB.ToList();
                    }
                    else
                    {
                        var elementsInMainDB = from el in mainScreenEntity.MainTables
                                               orderby el.Actual_Week ascending
                                               select el;
                        MainDatabaseXAML.ItemsSource = elementsInMainDB.ToList();
                    }
                }
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        public WPFDBAppCK()
        {
            InitializeComponent();

            VisibilityCalendarViewModel calendarView = new VisibilityCalendarViewModel();
            this.DataContext = calendarView;

        }


        private void RunDB_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {

                if (loadedExcelFile.Count == 0)
                {
                    showFinishedItemsOrAll();
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
                        loadedExcelFile.Clear(); //Clearing whole variable(List)

                        showFinishedItemsOrAll();
                    }
                }


            }




        }
        private void AddNewElementToGrid_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
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
                            Quantity = Convert.ToInt32(TextElementQuantity.Text),
                            IsFinished = false
                        };
                        if (loadedExcelFile.Count == 0) //Dependency wheter open was clicked or not
                        {

                            mainScreenEntity.MainTables.Add(mainTableAdd);
                            mainScreenEntity.SaveChanges();
                        }
                        else
                        {
                            loadedExcelFile.Add(mainTableAdd);
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
                if (loadedExcelFile.Count == 0)
                {
                    //Work on base database in entity

                    showFinishedItemsOrAll();
                }
                else
                {
                    //Add entry to excel file that was opened
                    MainDatabaseXAML.ItemsSource = loadedExcelFile;
                    MainDatabaseXAML.Items.Refresh();
                }
            }

        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {

                if (loadedExcelFile.Count == 0)
                {
                    //Working on MainDataBase - Entity

                    MainTable mainTableDel = (MainTable)MainDatabaseXAML.SelectedItem;
                    mainScreenEntity.MainTables.Attach(mainTableDel);
                    mainScreenEntity.MainTables.Remove(mainTableDel);
                    mainScreenEntity.SaveChanges();

                    //Displaying                    
                    showFinishedItemsOrAll();
                }
                else
                {
                    //Working on loaded file
                    loadedExcelFile.RemoveAt(MainDatabaseXAML.SelectedIndex);
                    MainDatabaseXAML.ItemsSource = loadedExcelFile;
                    MainDatabaseXAML.Items.Refresh();
                }

            }

        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {
                try
                {
                    MainTable mainTableUpdate = (MainTable)MainDatabaseXAML.SelectedItem;

                    if (loadedExcelFile.Count == 0)
                    {

                        mainScreenEntity.MainTables.Attach(mainTableUpdate);

                        mainTableUpdate.Planned_Week = Convert.ToInt32(TextElementPlannedWeek.Text);
                        mainTableUpdate.Actual_Week = Convert.ToInt32(TextElementActualWeek.Text);
                        mainTableUpdate.Weight = Convert.ToDouble(TextElementWeight.Text);
                        mainTableUpdate.Order = TextElementOrder.Text;
                        mainTableUpdate.Client_Name = TextElementClientName.Text;
                        mainTableUpdate.Name = TextElementName.Text;
                        mainTableUpdate.Quantity = Convert.ToInt32(TextElementQuantity.Text);


                        mainScreenEntity.SaveChanges();

                        TextElementPlannedWeek.Text = string.Empty;
                        TextElementActualWeek.Text = string.Empty;
                        TextElementWeight.Text = string.Empty;
                        TextElementOrder.Text = string.Empty;
                        TextElementClientName.Text = string.Empty;
                        TextElementName.Text = string.Empty;
                        TextElementQuantity.Text = string.Empty;

                        //Displaying
                        showFinishedItemsOrAll();

                        mainScreenEntity.Dispose();
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

                        //newSourceFile values are replaced by mainTableUpdate that is string replaced elements
                        //(new System.Collections.Generic.Mscorlib_CollectionDebugView<DBAppCK.MainTable>(newSourceFile).Items[0]).Client_Name <- !!Debugging!!
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Planned_Week = mainTableUpdate.Planned_Week;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Actual_Week = mainTableUpdate.Actual_Week;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Weight = mainTableUpdate.Weight;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Order = mainTableUpdate.Order;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Client_Name = mainTableUpdate.Client_Name;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Name = mainTableUpdate.Name;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Quantity = mainTableUpdate.Quantity;

                        //Clearing text boxes
                        TextElementPlannedWeek.Text = string.Empty;
                        TextElementActualWeek.Text = string.Empty;
                        TextElementWeight.Text = string.Empty;
                        TextElementOrder.Text = string.Empty;
                        TextElementClientName.Text = string.Empty;
                        TextElementName.Text = string.Empty;
                        TextElementQuantity.Text = string.Empty;

                        //Displaying source and refreshing so that values are visable
                        MainDatabaseXAML.ItemsSource = loadedExcelFile;
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
            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {
                MainTable mainTableDisplayRowAt = (MainTable)MainDatabaseXAML.SelectedItem;
                if (mainTableDisplayRowAt != null)
                {
                    mainScreenEntity.MainTables.Attach(mainTableDisplayRowAt);

                    TextElementPlannedWeek.Text = Convert.ToString(mainTableDisplayRowAt.Planned_Week);
                    TextElementActualWeek.Text = Convert.ToString(mainTableDisplayRowAt.Actual_Week);
                    TextElementWeight.Text = Convert.ToString(mainTableDisplayRowAt.Weight);
                    TextElementOrder.Text = mainTableDisplayRowAt.Order;
                    TextElementClientName.Text = mainTableDisplayRowAt.Client_Name;
                    TextElementName.Text = mainTableDisplayRowAt.Name;
                    TextElementQuantity.Text = Convert.ToString(mainTableDisplayRowAt.Quantity);
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {

                var SelectedItem = (ComboBoxItem)searchList.SelectedValue;
                if (SelectedItem == null)
                {
                    MessageBox.Show("Select item from box above !");
                }
                else
                {

                    var selectedTextToSearch = ((TextBlock)SelectedItem.Content).Text; //Debugging window shows exact variable
                    SearchModel searchModel = new SearchModel();

                    if (loadedExcelFile.Count == 0)
                    {
                        if (selectedTextToSearch != string.Empty)
                        {

                            var filterSource = searchModel.searchMainGrid(selectedTextToSearch, SearchItems.Text);
                            MainDatabaseXAML.ItemsSource = filterSource;
                        }
                    }
                    else if (loadedExcelFile.Count != 0)
                    {
                        if (selectedTextToSearch != string.Empty)
                        {

                            var filterSourceExternal = searchModel.searchExternalFile(loadedExcelFile, selectedTextToSearch, SearchItems.Text);
                            MainDatabaseXAML.ItemsSource = filterSourceExternal;
                            MainDatabaseXAML.Items.Refresh();
                        }
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


            var selectedItemInCombobox = (ComboBoxItem)searchList.SelectedValue; //Casting SearchList because content is too deep

            var selectedItem = ((TextBlock)selectedItemInCombobox.Content).Text; //Debugging window shows exact variable
            

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

            //
            //Still async implementation needed
            //

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel|*.xlsx";
            saveFileDialog.ShowDialog(); //Display File dialog with possible file format (Excel only)
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {

                    MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities();

                    if (loadedExcelFile.Count == 0)
                    {
                        var source = mainScreenEntity.MainTables.ToList(); // List from entity

                        //Inicialization
                        workbook.AddWorksheet("Table");
                        var worksheetSave = workbook.Worksheet("Table");
                        var range = worksheetSave.Cell("A2").InsertTable(source, true);
                        worksheetSave.Columns().AdjustToContents();


                        //Cell/View manipulation HEADER
                        worksheetSave.Cells("A1").Value = "Production Plan - Report based on Main DataBase";
                        worksheetSave.Range("A1:H1").Merge();
                        worksheetSave.Range("A1:H1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheetSave.Range("A1:H1").Style.Fill.SetBackgroundColor(XLColor.BlueGray);
                        worksheetSave.Range("A1:H1").Style.Font.FontSize = 16;
                        worksheetSave.Range("A1:H1").Style.Font.Bold = true;
                        worksheetSave.Range("A1:H1").Style.Font.FontColor = XLColor.Blue;

                        //Layout manipulation - in development
                        worksheetSave.Row(2).Style.Fill.SetBackgroundColor(XLColor.DarkGray);

                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                    else
                    {
                        //Inicialization
                        workbook.AddWorksheet("Table");
                        var worksheetSave = workbook.Worksheet("Table");
                        var range = worksheetSave.Cell("A2").InsertTable(loadedExcelFile, true);
                        worksheetSave.Columns().AdjustToContents();

                        //Cell/View manipulation HEADER
                        worksheetSave.Cells("A1").Value = "Production Plan - Report based on Main DataBase";
                        worksheetSave.Range("A1:H1").Merge();
                        worksheetSave.Range("A1:H1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheetSave.Range("A1:H1").Style.Fill.SetBackgroundColor(XLColor.BlueGray);
                        worksheetSave.Range("A1:H1").Style.Font.FontSize = 16;
                        worksheetSave.Range("A1:H1").Style.Font.Bold = true;
                        worksheetSave.Range("A1:H1").Style.Font.FontColor = XLColor.Blue;

                        //Layout manipulation - in development
                        worksheetSave.Row(2).Style.Fill.SetBackgroundColor(XLColor.DarkGray);

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
                    MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities();
                    List<MainTable> openList = new List<MainTable>();


                    var worksheetOpen = workbook.Worksheet(1); //Use 1 sheet in chosen pathfile

                    int rowStart = 3; //Skip header and names
                    int columnStart = 1;



                    while (string.IsNullOrWhiteSpace(worksheetOpen.Cell(rowStart, columnStart).Value?.ToString()) == false) // Is thre data in row? ID number
                    {
                        MainTable tableOpen = new MainTable();

                        tableOpen.Id = int.Parse(worksheetOpen.Cell(rowStart, columnStart).Value.ToString()); // change value from double to string, and then to int
                        tableOpen.Planned_Week = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 1).Value.ToString());
                        tableOpen.Actual_Week = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 2).Value.ToString());
                        tableOpen.Weight = double.Parse(worksheetOpen.Cell(rowStart, columnStart + 3).Value.ToString());
                        tableOpen.Order = worksheetOpen.Cell(rowStart, columnStart + 4).Value.ToString();
                        tableOpen.Client_Name = worksheetOpen.Cell(rowStart, columnStart + 5).Value.ToString();
                        tableOpen.Name = worksheetOpen.Cell(rowStart, columnStart + 6).Value.ToString();
                        tableOpen.Quantity = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 7).Value.ToString());

                        openList.Add(tableOpen);
                        rowStart++;
                    }

                    MainDatabaseXAML.ItemsSource = openList;

                    loadedExcelFile = openList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        private void Test_Click(object sender, RoutedEventArgs e)
        {

        }





        private void FinishedChecker_Click(object sender, RoutedEventArgs e)
        {

            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {

                MainTable mainTableCheck = (MainTable)MainDatabaseXAML.SelectedItem;

                mainTableCheck.IsFinished = mainTableCheck.IsFinished.Value;
                mainScreenEntity.MainTables.Attach(mainTableCheck);
                mainScreenEntity.MainTables.Remove(mainTableCheck);

                mainScreenEntity.SaveChanges();
                mainScreenEntity.MainTables.Add(mainTableCheck);
                mainScreenEntity.SaveChanges();

                //Displaying                
                showFinishedItemsOrAll();

                mainScreenEntity.Dispose();
            }


        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TextElementPlannedWeek.Text = string.Empty;
            TextElementActualWeek.Text = string.Empty;
            TextElementWeight.Text = string.Empty;
            TextElementOrder.Text = string.Empty;
            TextElementClientName.Text = string.Empty;
            TextElementName.Text = string.Empty;
            TextElementQuantity.Text = string.Empty;
        }

        private void DragnMove(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void FinishedOnly_Click(object sender, RoutedEventArgs e)
        {
            showFinishedItemsOrAll();
        }


        private void weekCalendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CalendarWeekModel selectedWeek = new CalendarWeekModel();
            var clickedDate = (DateTime)weekCalendar.SelectedDate;
            string week = "Week: ";

            weekCalendarTextBox.Text = week + selectedWeek.GetWeekNumber(clickedDate).ToString();

            TextElementPlannedWeek.Text = selectedWeek.GetWeekNumber(clickedDate).ToString();
            TextElementActualWeek.Text = selectedWeek.GetWeekNumber(clickedDate).ToString();
        }




        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            VisibilityCalendarViewModel calendarView = new VisibilityCalendarViewModel();
            if (calendarVisibilityFlag == false)
            {
                calendarView.ShowCalendar = Visibility.Visible;
                this.DataContext = calendarView.ShowCalendar;

                calendarVisibilityFlag = true;
            }
            else if (calendarVisibilityFlag == true)
            {
                this.DataContext = calendarView;
                calendarVisibilityFlag = false;
            }
        }
    }
}
