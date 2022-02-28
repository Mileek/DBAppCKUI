using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReSCat.Classes
{
    public class LogsModel
    {

        private static List<MainTable> _listLog = new List<MainTable>();

        public static List<MainTable> ListLog
        {
            get
            {
                return _listLog;
            }
            set
            {
                _listLog = value;
            }
        }

        private static MainTable _logAdd;

        public static MainTable LogAdd
        {
            get
            {
                return _logAdd;
            }
            set
            {
                _logAdd = value;
                value.Log = DateTime.Now + ": *Added* ";
                _listLog.Add(value);
            }
        }

        private static MainTable _logDel;

        public static MainTable LogDel
        {
            get
            {
                return _logDel;
            }
            set
            {
                _logDel = value;
                value.Log = DateTime.Now + ": *Deleted* ";
                _listLog.Add(value);
            }
        }
        
        //Does not work because i do not know what should trigger this event

        //private static MainTable _logUpdateBefore;

        //public static MainTable LogUpdateBefore
        //{
        //    get
        //    {
        //        return _logUpdateBefore;
        //    }
        //    set
        //    {
        //        _logUpdateBefore = value;
        //        value.Log = DateTime.Now + ": *Updated From:* ";
        //        //string before = value.Planned_Week.ToString() +
        //        //    value.Actual_Week.ToString() +
        //        //    value.Weight.ToString() +
        //        //    value.Order.ToString() +
        //        //    value.Client_Name +
        //        //    value.Name +
        //        //    value.Hall.ToString() +
        //        //    value.Quantity.ToString();
        //        ListLog.Add(value);
        //    }
        //}

        private static MainTable _logUpdate;

        public static MainTable LogUpdate
        {
            get
            {
                return _logUpdate;
            }
            set
            {
                _logUpdate = value;
                value.Log = DateTime.Now + ": *Update* ";
                _listLog.Add(value);
            }
        }
    }
}
