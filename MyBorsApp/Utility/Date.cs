using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Tools
{
    public static class Date
    {
        public static string toshamsi(this DateTime date)
        {
            PersianCalendar p =new PersianCalendar();
            string result = p.GetYear(date)+"/"+p.GetMonth(date)+"/"+p.GetDayOfMonth(date).ToString("0#");
            return result;
        }
    }
}