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
using ReSCat.Model;
using ReSCat.View;
using ReSCat.ViewModel;
using Microsoft.Win32;

namespace ReSCat.View
{
    /// <summary>
    /// Logika interakcji dla klasy ReSCatMainWindow.xaml
    /// </summary>

    //
    //Unique ID don't need to be "Recycled" and the amount of unique records is so big that it is not necessary (to delete it).
    //

    public partial class ReSCatMainWindow : Window
    {
        //Variables
        private List<MainTable> loadedExcelFile = new List<MainTable>(); //Public variable that store list from external excel file
        private string fileNamePath;
        private VisibilityCalendarViewModel visibility = new VisibilityCalendarViewModel();
        private VisibilityDataBaseViewModel dataBaseView = new VisibilityDataBaseViewModel();
        private bool calculatorWindowFlag = false;
        private bool notesWindowFlag = false;
        private readonly CalculatorView calculatorWindow = new CalculatorView();
        private readonly NotesView notesWindow = new NotesView();



        //Methods
        private void ShowFinishedItemsOrAll()
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
        private XLWorkbook SaveAsExcel(XLWorkbook workbook) // Function also had cell/row manipulation
        {
            MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities();

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

            return workbook;
        }
        private XLWorkbook SaveAsEditedExcel(XLWorkbook workbook)
        {
            MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities();

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

            return workbook;
        }


        private MainTable AddItemToDataBase()
        {
            MainTable mainTableAdd = new MainTable()
            {
                Planned_Week = Convert.ToInt32(TextElementPlannedWeek.Text),
                Actual_Week = Convert.ToInt32(TextElementActualWeek.Text),
                Weight = Convert.ToDouble(TextElementWeight.Text),
                Order = TextElementOrder.Text,
                Client_Name = TextElementClientName.Text,
                Name = TextElementName.Text,
                Hall = ((TextBlock)((ComboBoxItem)hallList.SelectedValue).Content).Text, // It could be build from 2 variables at the beggining
                Quantity = Convert.ToInt32(TextElementQuantity.Text),
                IsFinished = false
            };
            return mainTableAdd;
        }
        private void ClearTextNotValue()
        {
            TextElementPlannedWeek.Text = string.Empty;
            TextElementActualWeek.Text = string.Empty;
            TextElementWeight.Text = string.Empty;
            TextElementQuantity.Text = string.Empty;
        }

        private void EmptyAllTextBoxes()
        {
            TextElementPlannedWeek.Text = string.Empty;
            TextElementActualWeek.Text = string.Empty;
            TextElementWeight.Text = string.Empty;
            TextElementOrder.Text = string.Empty;
            TextElementClientName.Text = string.Empty;
            TextElementName.Text = string.Empty;
            TextElementQuantity.Text = string.Empty;
        }

        private MainTable UpdateRecordInDataBase(MainTable mainTableUpdate)
        {


            mainTableUpdate.Planned_Week = Convert.ToInt32(TextElementPlannedWeek.Text);
            mainTableUpdate.Actual_Week = Convert.ToInt32(TextElementActualWeek.Text);
            mainTableUpdate.Weight = Convert.ToDouble(TextElementWeight.Text);
            mainTableUpdate.Order = TextElementOrder.Text;
            mainTableUpdate.Client_Name = TextElementClientName.Text;
            mainTableUpdate.Name = TextElementName.Text;
            mainTableUpdate.Hall = ((TextBlock)((ComboBoxItem)hallList.SelectedValue).Content).Text; //again it might be replaced to combobox value selected, then content
            mainTableUpdate.Quantity = Convert.ToInt32(TextElementQuantity.Text);

            return mainTableUpdate;
        }

        private MainTable DisplaySelectedInTextBoxes(MainTable mainTableDisplayRowAt)
        {
            TextElementPlannedWeek.Text = Convert.ToString(mainTableDisplayRowAt.Planned_Week);
            TextElementActualWeek.Text = Convert.ToString(mainTableDisplayRowAt.Actual_Week);
            TextElementWeight.Text = Convert.ToString(mainTableDisplayRowAt.Weight);
            TextElementOrder.Text = mainTableDisplayRowAt.Order;
            TextElementClientName.Text = mainTableDisplayRowAt.Client_Name;
            TextElementName.Text = mainTableDisplayRowAt.Name;
            TextElementQuantity.Text = Convert.ToString(mainTableDisplayRowAt.Quantity);
            return mainTableDisplayRowAt;
        }
        private XLWorkbook OpenFile(XLWorkbook workbook)
        {
            MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities();
            List<MainTable> openList = new List<MainTable>();


            var worksheetOpen = workbook.Worksheet(1); //Use 1 sheet in chosen pathfile

            int rowStart = 3; //Skip header and names
            int columnStart = 1;


            //this works only for small tables ~1000 rows, because its speed
            mainScreenEntity.MainTables.RemoveRange(mainScreenEntity.MainTables);

            mainScreenEntity.SaveChanges();


            while (string.IsNullOrWhiteSpace(worksheetOpen.Cell(rowStart, columnStart).Value?.ToString()) == false) // Is thre data in row? ID number
            {
                MainTable tableOpen = new MainTable()
                {

                    Id = int.Parse(worksheetOpen.Cell(rowStart, columnStart).Value.ToString()), // change value from double to string, and then to int
                    Planned_Week = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 1).Value.ToString()),
                    Actual_Week = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 2).Value.ToString()),
                    Weight = double.Parse(worksheetOpen.Cell(rowStart, columnStart + 3).Value.ToString()),
                    Order = worksheetOpen.Cell(rowStart, columnStart + 4).Value.ToString(),
                    Client_Name = worksheetOpen.Cell(rowStart, columnStart + 5).Value.ToString(),
                    Name = worksheetOpen.Cell(rowStart, columnStart + 6).Value.ToString(),
                    Hall = worksheetOpen.Cell(rowStart, columnStart + 7).Value.ToString(),
                    Quantity = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 8).Value.ToString())
                };

                mainScreenEntity.MainTables.Add(tableOpen);
                mainScreenEntity.SaveChanges();
                rowStart++;
            }
            return workbook;
        }
        private void DataBaseMenuVisibility()
        {
            MainDatabaseXAML.Visibility = dataBaseView.ShowDataBaseMenu;
            labelInfoPanel.Visibility = dataBaseView.ShowDataBaseMenu;
            typeDataBoxes.Visibility = dataBaseView.ShowDataBaseMenu;
            modifyRecordsButtons.Visibility = dataBaseView.ShowDataBaseMenu;
            extraFunctionButtons.Visibility = dataBaseView.ShowDataBaseMenu;
        }


        //Controls
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            weekCalendarTextBox.Visibility = Visibility.Hidden;
            weekCalendar.Visibility = Visibility.Hidden;
        }

        public ReSCatMainWindow()
        {
            InitializeComponent();

        }


        private void RunDB_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {
                if (loadedExcelFile.Count == 0)
                {
                    ShowFinishedItemsOrAll();
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

                        ShowFinishedItemsOrAll();
                    }
                }
            }
        }

        private void AddNewElementToGrid_Click(object sender, RoutedEventArgs e)
        {
            using (MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities())
            {
                if (TextElementPlannedWeek.Text == String.Empty //Check if any textbox is empty
                    || TextElementActualWeek.Text == String.Empty
                    || TextElementWeight.Text == String.Empty
                    || TextElementOrder.Text == String.Empty
                    || TextElementClientName.Text == String.Empty
                    || TextElementName.Text == String.Empty
                    || TextElementQuantity.Text == String.Empty)
                {
                    MessageBox.Show("Enter missing data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt32(TextElementPlannedWeek.Text) <= 0 // check if appropriate textbox has value, not text
                || Convert.ToInt32(TextElementActualWeek.Text) <= 0
                || Convert.ToDouble(TextElementWeight.Text) <= 0
                || Convert.ToInt32(TextElementQuantity.Text) <= 0)
                {
                    ClearTextNotValue();
                    MessageBox.Show("Planned week, Actual Week, Weight and Quantity can not be negative!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    if (loadedExcelFile.Count == 0) //Dependency wheter open was clicked or not
                    {
                        mainScreenEntity.MainTables.Add(AddItemToDataBase());
                        mainScreenEntity.SaveChanges();
                    }
                    else
                    {
                        loadedExcelFile.Add(AddItemToDataBase());
                    }

                    //Displaying
                    if (loadedExcelFile.Count == 0)
                    {
                        //Work on base database in entity

                        ShowFinishedItemsOrAll();
                    }
                    else
                    {
                        //Add entry to excel file that was opened
                        MainDatabaseXAML.ItemsSource = loadedExcelFile;
                        MainDatabaseXAML.Items.Refresh();
                    }
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
                    if (mainTableDel == null)
                    {
                        MessageBox.Show("You need to run data base first!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {

                        mainScreenEntity.MainTables.Attach(mainTableDel);
                        mainScreenEntity.MainTables.Remove(mainTableDel);
                        mainScreenEntity.SaveChanges();

                        //Displaying                    
                        ShowFinishedItemsOrAll();
                    }
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

                        UpdateRecordInDataBase(mainTableUpdate);

                        mainScreenEntity.SaveChanges();

                        EmptyAllTextBoxes();

                        //Displaying
                        ShowFinishedItemsOrAll();

                        mainScreenEntity.Dispose();
                    }
                    else
                    {
                        //mainTableUpdate is storing items that are replaced
                        UpdateRecordInDataBase(mainTableUpdate);

                        //newSourceFile values are replaced by mainTableUpdate that is string replaced elements
                        //(new System.Collections.Generic.Mscorlib_CollectionDebugView<ReSCat.MainTable>(newSourceFile).Items[0]).Client_Name <- !!Debugging!!
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Planned_Week = mainTableUpdate.Planned_Week;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Actual_Week = mainTableUpdate.Actual_Week;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Weight = mainTableUpdate.Weight;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Order = mainTableUpdate.Order;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Client_Name = mainTableUpdate.Client_Name;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Name = mainTableUpdate.Name;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Hall = mainTableUpdate.Hall;
                        loadedExcelFile[MainDatabaseXAML.SelectedIndex].Quantity = mainTableUpdate.Quantity;

                        //Clearing text boxes
                        EmptyAllTextBoxes();

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

                    DisplaySelectedInTextBoxes(mainTableDisplayRowAt);
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

        private void SearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            using (XLWorkbook workbook = new XLWorkbook())
            {
                if (!File.Exists(fileNamePath)) //File does not exist, was deleted renamed etc
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel|*.xlsx"
                    };
                    saveFileDialog.ShowDialog(); //Display File dialog with possible file format (Excel only)
                    try
                    {

                        if (loadedExcelFile.Count == 0)
                        {
                            SaveAsExcel(workbook).SaveAs(saveFileDialog.FileName);

                            fileNamePath = saveFileDialog.FileName; //Get file name path to variable
                        }
                        else
                        {
                            SaveAsEditedExcel(workbook).SaveAs(saveFileDialog.FileName);

                            fileNamePath = saveFileDialog.FileName; //Get file name path to variable
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (File.Exists(fileNamePath))
                {
                    SaveAsExcel(workbook).SaveAs(fileNamePath);
                }
                else
                {
                    MessageBox.Show("File does not exist!");
                }

            }

        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {

            //
            //Still async implementation needed
            //

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel|*.xlsx"
            };
            saveFileDialog.ShowDialog(); //Display File dialog with possible file format (Excel only)
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    if (loadedExcelFile.Count == 0)
                    {
                        SaveAsExcel(workbook).SaveAs(saveFileDialog.FileName);

                        fileNamePath = saveFileDialog.FileName; //Get file name path to variable
                    }
                    else
                    {
                        SaveAsEditedExcel(workbook).SaveAs(saveFileDialog.FileName);

                        fileNamePath = saveFileDialog.FileName; //Get file name path to variable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel|*.xlsx"
            };
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
                        MainTable tableOpen = new MainTable
                        {
                            Id = int.Parse(worksheetOpen.Cell(rowStart, columnStart).Value.ToString()), // change value from double to string, and then to int
                            Planned_Week = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 1).Value.ToString()),
                            Actual_Week = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 2).Value.ToString()),
                            Weight = double.Parse(worksheetOpen.Cell(rowStart, columnStart + 3).Value.ToString()),
                            Order = worksheetOpen.Cell(rowStart, columnStart + 4).Value.ToString(),
                            Client_Name = worksheetOpen.Cell(rowStart, columnStart + 5).Value.ToString(),
                            Name = worksheetOpen.Cell(rowStart, columnStart + 6).Value.ToString(),
                            Hall = worksheetOpen.Cell(rowStart, columnStart + 7).Value.ToString(),
                            Quantity = int.Parse(worksheetOpen.Cell(rowStart, columnStart + 8).Value.ToString())
                        };

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
                ShowFinishedItemsOrAll();

                mainScreenEntity.Dispose();
            }


        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            EmptyAllTextBoxes();
        }

        private void DragnMove(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void FinishedOnly_Click(object sender, RoutedEventArgs e)
        {
            ShowFinishedItemsOrAll();
        }

        private void WeekCalendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CalendarWeekModel selectedWeek = new CalendarWeekModel();
            var clickedDate = (DateTime)weekCalendar.SelectedDate;
            string week = "Week: ";

            weekCalendarTextBox.Text = week + selectedWeek.GetWeekNumber(clickedDate).ToString();

            TextElementPlannedWeek.Text = selectedWeek.GetWeekNumber(clickedDate).ToString();
            TextElementActualWeek.Text = selectedWeek.GetWeekNumber(clickedDate).ToString();
        }

        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            if (visibility.ShowCalendar == Visibility.Visible)
            {
                visibility.ShowCalendar = Visibility.Hidden;
                weekCalendarTextBox.Visibility = visibility.ShowCalendar;
                weekCalendar.Visibility = visibility.ShowCalendar;
                DataContext = visibility;
            }
            else
            {
                visibility.ShowCalendar = Visibility.Visible;
                weekCalendarTextBox.Visibility = visibility.ShowCalendar;
                weekCalendar.Visibility = visibility.ShowCalendar;
                DataContext = visibility;
            }
        }

        private void CalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (systemCalculator.IsChecked == false)
            {
                System.Diagnostics.Process.Start("calc.exe");
                calculatorWindow.Hide();
                calculatorWindowFlag = false;
            }
            else if (systemCalculator.IsChecked == true)
            {
                if (calculatorWindowFlag == false)
                {
                    calculatorWindow.Show();
                    calculatorWindowFlag = true;
                }
                else if (calculatorWindowFlag == true)
                {
                    calculatorWindow.Hide();
                    calculatorWindowFlag = false;
                }
            }
        }

        private void NotesButton_Click(object sender, RoutedEventArgs e)
        {

            if (notesWindowFlag == false)
            {
                notesWindow.Show();
                notesWindowFlag = true;
            }
            else if (notesWindowFlag == true)
            {
                notesWindow.Hide();
                notesWindowFlag = false;
            }
        }

        private void HallList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to overwrite main table?, " +
                "All non-saved data will be deleted." +
                "You might want to EDIT data.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.No)
            {
                MessageBox.Show("Opening was canceled.");
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel|*.xlsx"
                };
                openFileDialog.ShowDialog();
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {

                        OpenFile(workbook);

                        ShowFinishedItemsOrAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to overwrite main table?, " +
                "All non-saved data will be deleted.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.No)
            {
                MessageBox.Show("Creating new file was canceled.");
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel|*.xlsx"
                };
                saveFileDialog.ShowDialog(); //Display File dialog with possible file format (Excel only)
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities();

                        //this works only for small tables ~1000 rows, because its speed, might as well use await 
                        mainScreenEntity.MainTables.RemoveRange(mainScreenEntity.MainTables);

                        mainScreenEntity.SaveChanges();

                        SaveAsExcel(workbook).SaveAs(saveFileDialog.FileName);

                        fileNamePath = saveFileDialog.FileName; //Get file name path to variable

                        OpenFile(workbook);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DataBaseMenu_Click(object sender, RoutedEventArgs e)
        {
            logsMenu.IsChecked = false;
            chartsMenu.IsChecked = false;
            if ((bool)dataBaseMenu.IsChecked == true)
            {
                MainWindow.Content = new UserControl();

                dataBaseView.ShowDataBaseMenu = Visibility.Visible;
                DataBaseMenuVisibility();

            }
            else if ((bool)dataBaseMenu.IsChecked == false)
            {
                MainWindow.Content = new UserControl();

                dataBaseView.ShowDataBaseMenu = Visibility.Hidden;

                DataBaseMenuVisibility();

                weekCalendarTextBox.Visibility = Visibility.Hidden;
                weekCalendar.Visibility = Visibility.Hidden;

                visibility.ShowCalendar = Visibility.Hidden;
            }

        }

        private void LogsMenu_Click(object sender, RoutedEventArgs e)
        {
            dataBaseMenu.IsChecked = false;
            chartsMenu.IsChecked = false;

            if ((bool)dataBaseMenu.IsChecked == false)
            {
                dataBaseView.ShowDataBaseMenu = Visibility.Hidden;
                DataBaseMenuVisibility();

                weekCalendarTextBox.Visibility = Visibility.Hidden;
                weekCalendar.Visibility = Visibility.Hidden;

                visibility.ShowCalendar = Visibility.Hidden;
            }
            if (logsMenu.IsChecked == true)
            {
                MainWindow.Content = new LogsView();
            }
            else if (logsMenu.IsChecked == false)
            {
                MainWindow.Content = new UserControl();
                dataBaseMenu.IsChecked = true;

                dataBaseView.ShowDataBaseMenu = Visibility.Visible;

                DataBaseMenuVisibility();

            }

        }

        private void ChartsMenu_Click(object sender, RoutedEventArgs e)
        {
            dataBaseMenu.IsChecked = false;
            logsMenu.IsChecked = false;

            if ((bool)dataBaseMenu.IsChecked == false)
            {
                dataBaseView.ShowDataBaseMenu = Visibility.Hidden;

                DataBaseMenuVisibility();

                weekCalendarTextBox.Visibility = Visibility.Hidden;
                weekCalendar.Visibility = Visibility.Hidden;

                visibility.ShowCalendar = Visibility.Hidden;
            }

            if (chartsMenu.IsChecked == true)
            {
                MainWindow.Content = new ChartMenuView();
            }
            else if (chartsMenu.IsChecked == false)
            {
                MainWindow.Content = new UserControl();
                dataBaseMenu.IsChecked = true;

                dataBaseView.ShowDataBaseMenu = Visibility.Visible;

                DataBaseMenuVisibility();
            }

        }
    }
}
