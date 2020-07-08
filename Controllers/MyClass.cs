using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RayaHesab.Controllers
{
    public static class MyClass
    {
        public static string PersianToEnglish(this string persianStr)
        {
            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9',
                ['0'] = '0',
                ['1'] = '1',
                ['2'] = '2',
                ['3'] = '3',
                ['4'] = '4',
                ['5'] = '5',
                ['6'] = '6',
                ['7'] = '7',
                ['8'] = '8',
                ['9'] = '9',
                ['/'] = '/'
            };
            foreach (var item in persianStr)
            {
                persianStr = persianStr.Replace(item, LettersDictionary[item]);
            }
            return persianStr;
        }
        public static string getpersiondate(DateTime d)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(d) + "/" + ("0" + pc.GetMonth(d)).Right(2) + "/" + ("0" + pc.GetDayOfMonth(d)).Right(2);
        }
        public static string getpersionMonth(DateTime d, int t, int mMonth = 0)
        {
            PersianCalendar pc = new PersianCalendar();
            int m = pc.GetMonth(d) - mMonth;
            int y = pc.GetYear(d);
            int day = 1;
            string str;
            if (t != 0)
                day = 31;
            if (m < 1)
            {
                y = y - 1;
                m = m + 12;
            };
            str = y + "/" + ("0" + m).Right(2) +"/"+ ("0" + day).Right(2);
            return str;
        }
        public static string getpersionYear(DateTime d, int t , int c = 0 )
        {
            PersianCalendar pc = new PersianCalendar();
            int m = 1;
            int y = pc.GetYear(d);
            y = y - c;
            int day = 1;
            string str;
            if (t != 0)
            {
                day = 30;
                m = 12;
            }
            str = y + "/" + ("0" + m).Right(2) +"/"+ ("0" + day).Right(2);
            return str;
        }

        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }
    }
}
