namespace POSBlazorWebAssembly.Models
{
    public class TodaySaleProductList
    {
        public List<int> product_count { get; set; } 
        public List<string> product_name { get; set; }
    }

    public class TodaySaleProductListModel
    {
        public int product_count { get; set; }
        public string product_name { get; set; }    
    }
}
