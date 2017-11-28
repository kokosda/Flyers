using System;
using System.Data;
using System.Globalization;
using System.Web;

namespace FlyerMe
{
    public static class StringHelper
    {
        public static String FormatPrice(this String price)
        {
            var result = String.Empty;

            if (price != null)
            {
                var prices = price.Split('|');

                if (prices.Length > 1)
                {
                    Decimal numericPrice;

                    if (Decimal.TryParse(prices[1], NumberStyles.AllowDecimalPoint, clsUtility.Culture, out numericPrice))
                    {
                        result = numericPrice.FormatLongPrice();
                    }
                }
            }

            return result;
        }

        public static String FormatPrice(this Decimal price)
        {
            var result =  price.ToString("C", clsUtility.OriginalCulture);

            return result;
        }

        public static String FormatLongPrice(this Decimal price)
        {
            String result;

            if (price < 1000000M && price % 1000M == 0M)
            {
                result = String.Format("{0}K", (price / 1000M).ToString("C0", clsUtility.OriginalCulture));
            }
            else
            {
                result = price.ToString("C0", clsUtility.OriginalCulture);
            }

            return result;
        }

        public static String FormatCount(this Int32 count)
        {
            var result = count.ToString("N0", clsUtility.Culture);

            return result;
        }

        public static String FormatDate(this DateTime dateTime)
        {
            var result = dateTime.ToString("M/d/yyyy");

            return result;
        }

        public static String Capitalize(this String value)
        {
            var result = value;

            if (!String.IsNullOrEmpty(value) && value.Length > 1)
            {
                result = String.Format("{0}{1}", Char.ToUpper(value[0]), value.Substring(1));
            }

            return result;
        }

        public static Uri ToUri(this String value)
        {
            Uri result = null;

            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    result = new UriBuilder(value).Uri;
                }
                catch
                {
                }
            }

            return result;
        }

        public static Boolean HasNoText(this String value)
        {
            return String.IsNullOrEmpty(value) || String.IsNullOrEmpty(value.Trim());
        }

        public static Boolean HasText(this String value)
        {
            return !value.HasNoText();
        }

        public static String GetStringFromDataColumn(this DataRow dataRow, Int32 columnIndex)
        {
            return dataRow[columnIndex] == DBNull.Value ? null : dataRow[columnIndex].ToString();
        }

        public static String GetDecimalStringFromDataColumn(this DataRow dataRow, Int32 columnIndex, String format)
        {
            return dataRow[columnIndex] == DBNull.Value ? null : ((Decimal)dataRow[columnIndex]).ToString(format);
        }

        public static String GetDateStringFromDataColumn(this DataRow dataRow, Int32 columnIndex)
        {
            return dataRow[columnIndex] == DBNull.Value ? null : ((DateTime)dataRow[columnIndex]).ToString("MM/dd/yyyy");
        }

        public static String GetDateStringFromDataColumn(this DataRow dataRow, String columnName)
        {
            return dataRow[columnName] == DBNull.Value ? null : ((DateTime)dataRow[columnName]).ToString("MM/dd/yyyy");
        }

        public static String GetDateTimeStringFromDataColumn(this DataRow dataRow, String columnName)
        {
            return dataRow[columnName] == DBNull.Value ? null : ((DateTime)dataRow[columnName]).ToString();
        }

        public static String GetPriceStringFromDataColumn(this DataRow dataRow, Int32 columnIndex)
        {
            return dataRow[columnIndex] == DBNull.Value ? null : ((Decimal)dataRow[columnIndex]).FormatPrice();
        }
    }
}