using BlazorWasm.MiniPOS.Models;

namespace BlazorWasm.MiniPOS.Pages.Dashboard.Model;

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

public class QtyOfTopFiveProductsByYearModel
{
    public string[] productNames { get; set; }

    public List<ProductInfo> productInfos { get; set; }
}

public class PastSevenDaysModel
{
    public string[] days { get; set; }

    public List<ProductInfo> productInfos { get; set; }
}

public class SixMostSoldProductsModel
{
    public string name { get; set; }
    public List<int> data { get; set; }
}

public class MaxMinQtyOfProductsModel
{
    public string name { get; set; }
    public int low { get; set; }
    public int high { get; set; }
}

public class ProductCategoryChartModel
{
    public string name { get; set; }
    public List<ProductChartModel> data { get; set; }
}

public class ProductChartModel
{
    public string name { get; set; }
    public int value { get; set; }
}

public class RootObject
{
    public string name { get; set; }
    public string lineColor { get; set; }
    public string color { get; set; }
    public string fillColor { get; set; }
    public object[][] data { get; set; }
    public int xAxis { get; set; }
}

public class MonthlyRevenueReportForThreeYear
{

    public string monthName { get; set; }
    public int value { get; set; }
}

public class MonthlyRevenueReportResponseModel
{
    public string name { get; set; }
    public List<MonthlyRevenueReportForThreeYear> data { get; set; }
}

