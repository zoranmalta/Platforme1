using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.util
{
    class StringUtils
    {
        public static String clean(String str)
        {
            if (str == null)
                return "";
            return str;
        }
        public static bool isInteger(String str)
        {
            try
            {
                int.Parse(str);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }
        public static bool isDouble(String str)
        {
            try
            {
                Double.Parse(str);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static bool isBoolean(String str)
        {
            try
            {
                Boolean.Parse(str);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        //public static String clean(DateTime d)
        //{
        //    if (d == null)
        //        return "";
        //    return formatDate(d);
        //}

        //public static String formatDate(DateTime d)
        //{
        //    if (d == null)
        //        return "";
        //    DateFormat formatter = new SimpleDateFormat("dd.MM.yyyy.HH.mm");
        //    return formatter.format(d);
        //}


        //public static Date parseDate(String str)
        //{
        //    if (str.equals(""))
        //        return null;
        //    DateFormat formatter = new SimpleDateFormat("dd.MM.yyyy.HH.mm");
        //    try
        //    {
        //        return formatter.parse(str);
        //    }
        //    catch (ParseException e)
        //    {
        //        e.printStackTrace();
        //    }
        //    return null;
        //}
    }
}
