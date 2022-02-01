using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAppCK.Model
{
    public class CalendarWeekModel
    {
        public int GetWeekNumber(DateTime time)
        {
            GregorianCalendar cal = new GregorianCalendar();
            int week = cal.GetWeekOfYear(time, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            return week;
        }
    }
}
