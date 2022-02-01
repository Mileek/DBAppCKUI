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
                var searchedElements = from el in mainScreenEntity.MainTables.ToList() where (Convert.ToString(el.Planned_Week).Contains(SearchItems)) orderby el.Actual_Week ascending select el;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Actual Week")
            {
                var searchedElements = from el in mainScreenEntity.MainTables.ToList() where (Convert.ToString(el.Actual_Week).Contains(SearchItems)) orderby el.Actual_Week ascending select el;
                return searchedElements.ToList();                

            }
            else if (selectedTextToSearch == "Weight")
            {
                var searchedElements = from el in mainScreenEntity.MainTables.ToList() where (Convert.ToString(el.Weight).Contains(SearchItems)) orderby el.Actual_Week ascending select el;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Order")
            {
                var searchedElements = from el in mainScreenEntity.MainTables where (el.Order.Contains(SearchItems)) orderby el.Actual_Week ascending select el;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Client Name")
            {
                var searchedElements = from el in mainScreenEntity.MainTables where (el.Client_Name.Contains(SearchItems)) orderby el.Actual_Week ascending select el;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Name")
            {
                var searchedElements = from el in mainScreenEntity.MainTables where (el.Name.Contains(SearchItems)) orderby el.Actual_Week ascending select el;
                return searchedElements.ToList();                
            }
            else if (selectedTextToSearch == "Quantity")
            {
                var searchedElements = from el in mainScreenEntity.MainTables.ToList() where (Convert.ToString(el.Quantity).Contains(SearchItems)) orderby el.Actual_Week ascending select el;
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
