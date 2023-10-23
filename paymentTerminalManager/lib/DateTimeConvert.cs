using System.Globalization;
using System.Net.NetworkInformation;

namespace PaymentTerminalManager.Lib;
public class DateTimeConvert
{
    public static DateTime ParsPersianDateTime(string date, string time)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            //default values base on today
            int yearNow = pc.GetYear(DateTime.Now);
            int monthNow = pc.GetMonth(DateTime.Now);
            int dayNow = pc.GetDayOfMonth(DateTime.Now);

            int hourNow = DateTime.Now.Hour;
            int minuteNow = DateTime.Now.Minute;

            int year = yearNow;
            int month = monthNow;
            int day = dayNow;

            int hour = hourNow;
            int minute = minuteNow;

            if (!string.IsNullOrEmpty(date))
            {
                System.String[] userDateParts = date.Split(new[] { "/" }, System.StringSplitOptions.None);
                if(!string.IsNullOrEmpty(userDateParts[0])) {
                    if(int.TryParse(userDateParts[0],out int parseYear)) {
                        year = parseYear;
                    }
                }
                if(!string.IsNullOrEmpty(userDateParts[1])) {
                    if(int.TryParse(userDateParts[1],out int parseMonth)) {
                        month = parseMonth;
                    }
                }
                if(!string.IsNullOrEmpty(userDateParts[2])) {
                    if(int.TryParse(userDateParts[2],out int parsDay)) {
                        day = parsDay;
                    }
                }
            }

            if(!string.IsNullOrEmpty(time))
            {
                System.String[] userDateParts = time.Split(new[] { ":" }, System.StringSplitOptions.None);
                if(!string.IsNullOrEmpty(userDateParts[0])) {
                    if(int.TryParse(userDateParts[0],out int parsHour)) {
                        hour = parsHour;
                    }
                }
                if(!string.IsNullOrEmpty(userDateParts[1])) {
                    if(int.TryParse(userDateParts[1],out int parsMinute)) {
                        minute = parsMinute;
                    }
                }
            }

            return pc.ToDateTime(year, month,day,hour,minute,0,0);
        }
        catch
        {
            return DateTime.Now;
        }
    }
}
