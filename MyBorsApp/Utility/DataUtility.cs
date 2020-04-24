using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;


namespace MyBorsApp.Convertor
{
    public static class DataUtility
    {
        public static bool HolidayTest(DateTime inDateTime)
        {
            
            PersianCalendar p = new PersianCalendar();
            //var miladi = p..(yeear, moonth, daay, 0, 0, 0, 0);
            //var week = persianCalendar.GetDayOfWeek(miladi);
            bool b;
            try
            {
                var url = "http://www.time.ir/fa/event/list/0/" + p.GetYear(inDateTime) + "/" + p.GetMonth(inDateTime) + "/" + p.GetDayOfMonth(inDateTime);
                var web = new HtmlWeb();

                var doc = web.Load(url);

                 b = doc.Text.Contains("eventHoliday");
            }
            catch
            {
                b = false;
            }
            bool c = Convert.ToString(p.GetDayOfWeek(inDateTime)).Contains("Friday");

           


            if (b == true || c == true)
            {
                
                return true;
            }
            else
            {
                
                return false;
            }
        }

        public static string totaltime(List<TimeSpan> timeSpans)
        {
            TimeSpan totaltime = TimeSpan.Parse("00:00");
            foreach (var VARIABLE in timeSpans)
            {
                totaltime += VARIABLE;
            }

            int hours = totaltime.Days * 24 + totaltime.Hours;
            int minutes = totaltime.Minutes;
            string result= String.Format("{0}:{1}", hours, minutes);
            return result;
        }

        public static bool IsEndWeakDay()
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                return true;
            }

            return false;
        }
    }
}
