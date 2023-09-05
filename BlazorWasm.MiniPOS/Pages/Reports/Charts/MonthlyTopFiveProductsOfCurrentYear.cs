namespace BlazorWasm.MiniPOS.Pages.Reports.Charts;

public class MonthlyTopFiveProductsOfCurrentYear
{
    public string Month { get; set; }
    public List<ProductInfo> TopProducts { get; set; }
}

public class ProductInfo
{
    public string name { get; set; }
    public int[] data { get; set; }
}