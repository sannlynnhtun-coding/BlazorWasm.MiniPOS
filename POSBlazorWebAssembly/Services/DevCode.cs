namespace POSBlazorWebAssembly.Services
{
    public static class DevCode
    {
        public static List<T> ToPage<T>(this List<T> lst, int pageNo, int pageSize)
        {
            int skipRowCount = (pageNo - 1) * pageSize;
            return lst.Skip(skipRowCount).Take(pageSize).ToList();
        }
    }
}
