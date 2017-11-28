using System;

namespace FlyerMe.SpecialExtensions
{
    public static class StringSpecialHelper
    {
        public static String GetDecimalStringFromObject(this Object @object, String format)
        {
            return @object == DBNull.Value || (!(@object is Decimal)) ? null : ((Decimal)@object).ToString(format);
        }

        public static String GetDateStringFromObject(this Object @object)
        {
            return @object == DBNull.Value || (!(@object is DateTime)) ? null : ((DateTime)@object).ToString("MM/dd/yyyy");
        }

        public static String GetDateTimeStringFromObject(this Object @object)
        {
            return @object == DBNull.Value || (!(@object is DateTime)) ? null : ((DateTime)@object).ToString();
        }

        public static String GetPriceStringFromObject(this Object @object)
        {
            return @object == DBNull.Value || (!(@object is Decimal)) ? null : ((Decimal)@object).FormatPrice();
        }

        public static String GetTrimmedStringFromObject(this Object @object)
        {
            return @object == DBNull.Value || (!(@object is String)) ? null : (@object as String).Trim();
        }

        public static String GetBooleanStringFromObject(this Object @object)
        {
            return @object == DBNull.Value || (!(@object is Boolean)) ? null : ((Boolean)@object).ToString();
        }

        public static String GetDateFormat()
        {
            return "MM/dd/yyyy";
        }
    }
}