using System.Globalization;

namespace BlazorWasm.MiniPOS.Services
{
    public static class DevCode
    {
        public static List<T> ToPage<T>(this List<T> lst, int pageNo, int pageSize)
        {
            var skipRowCount = (pageNo - 1) * pageSize;
            return lst.Skip(skipRowCount).Take(pageSize).ToList();
        }
        
        public static string GetMonthName(this int monthNumber)
        {
            if (monthNumber < 1 || monthNumber > 12)
            {
                return "Invalid month number";
            }

            DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;
            return dtfi.GetMonthName(monthNumber);
        }
    }
}
