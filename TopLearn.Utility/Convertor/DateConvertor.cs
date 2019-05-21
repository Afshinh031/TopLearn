using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TopLearn.Utility.Convertor
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime dateTime) {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(dateTime).ToString() + "/"
                + persianCalendar.GetMonth(dateTime).ToString("00") + "/"
                + persianCalendar.GetDayOfMonth(dateTime).ToString("00") + " "
                + persianCalendar.GetHour(dateTime).ToString() + ":"
                + persianCalendar.GetMinute(dateTime).ToString("00") + ":"
                + persianCalendar.GetSecond(dateTime).ToString("00");
        }
    }
}
