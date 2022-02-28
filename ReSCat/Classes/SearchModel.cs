using ReSCat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace ReSCat.Model
{
    public class SearchModel
    {
        MainDataBaseEntities mainScreenEntity = new MainDataBaseEntities();
        
        public List<MainTable> searchMainGrid(string selectedTextToSearch, string SearchItems)
        {
            if (selectedTextToSearch == "Planned Week")
            {
                var searchedElements = from element in mainScreenEntity.MainTables.ToList() where (Convert.ToString(element.Planned_Week).Contains(SearchItems)) orderby element.Planned_Week ascending select element;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Actual Week")
            {
                var searchedElements = from element in mainScreenEntity.MainTables.ToList() where (Convert.ToString(element.Actual_Week).Contains(SearchItems)) orderby element.Planned_Week ascending select element;
                return searchedElements.ToList();                

            }
            else if (selectedTextToSearch == "Weight")
            {
                var searchedElements = from element in mainScreenEntity.MainTables.ToList() where (Convert.ToString(element.Weight).Contains(SearchItems)) orderby element.Planned_Week ascending select element;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Order")
            {
                var searchedElements = from element in mainScreenEntity.MainTables where (element.Order.Contains(SearchItems)) orderby element.Planned_Week ascending select element;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Client Name")
            {
                var searchedElements = from element in mainScreenEntity.MainTables where (element.Client_Name.Contains(SearchItems)) orderby element.Planned_Week ascending select element;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Name")
            {
                var searchedElements = from element in mainScreenEntity.MainTables where (element.Name.Contains(SearchItems)) orderby element.Planned_Week ascending select element;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Hall")
            {
                var searchedElements = from element in mainScreenEntity.MainTables.ToList() where (Convert.ToString(element.Hall).Contains(SearchItems)) orderby element.Planned_Week ascending select element;
                return searchedElements.ToList();
            }
            else if (selectedTextToSearch == "Quantity")
            {
                var searchedElements = from element in mainScreenEntity.MainTables.ToList() where (Convert.ToString(element.Quantity).Contains(SearchItems)) orderby element.Planned_Week ascending select element;
                return searchedElements.ToList();                
            }
            else
            {
                return null;
            }
        }

        public List<MainTable> searchExternalFile(List<MainTable> loadedExcelFile, string selectedTextToSearch, string SearchItems)
        {

            if (selectedTextToSearch == "Planned Week")
            {
                var filterInFile = loadedExcelFile.Where(newSourceFile => (Convert.ToString(newSourceFile.Planned_Week).Contains(SearchItems)));
                return filterInFile.ToList();
            }
            else if (selectedTextToSearch == "Actual Week")
            {
                var filterInFile = loadedExcelFile.Where(newSourceFile => (Convert.ToString(newSourceFile.Actual_Week).Contains(SearchItems)));
                return filterInFile.ToList();
            }
            else if (selectedTextToSearch == "Weight")
            {
                var filterInFile = loadedExcelFile.Where(newSourceFile => (Convert.ToString(newSourceFile.Weight).Contains(SearchItems)));
                return filterInFile.ToList();
            }
            else if (selectedTextToSearch == "Order")
            {
                var filterInFile = loadedExcelFile.Where(newSourceFile => (newSourceFile.Order).Contains(SearchItems));
                return filterInFile.ToList();
            }
            else if (selectedTextToSearch == "Client Name")
            {
                var filterInFile = loadedExcelFile.Where(newSourceFile => (newSourceFile.Client_Name).Contains(SearchItems));
                return filterInFile.ToList();
            }
            else if (selectedTextToSearch == "Name")
            {
                var filterInFile = loadedExcelFile.Where(newSourceFile => (newSourceFile.Name).Contains(SearchItems));
                return filterInFile.ToList();
            }
            else if (selectedTextToSearch == "Hall")
            {
                var filterInFile = loadedExcelFile.Where(newSourceFile => (newSourceFile.Hall).Contains(SearchItems));
                return filterInFile.ToList();
            }
            else if (selectedTextToSearch == "Quantity")
            {
                var filterInFile = loadedExcelFile.Where(newSourceFile => (Convert.ToString(newSourceFile.Quantity).Contains(SearchItems)));
                return filterInFile.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
