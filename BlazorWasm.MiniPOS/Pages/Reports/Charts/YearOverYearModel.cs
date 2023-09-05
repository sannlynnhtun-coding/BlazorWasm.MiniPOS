namespace BlazorWasm.MiniPOS.Pages.Reports.Charts
{
    public class YearOverYearModel
    {
        public string Year { get; set; }
        public List<int>? Data { get; set; }
    }
    public class YearOverYearReturnModel
    {
        public List<string> Year { get; set; } = new List<string>();
        public List<YearOverYearModel> YearData { get; set; } = new List<YearOverYearModel>();
    }
    public class YearOverYearResponseModel
    {
        public int Year { get; set; }
        public int TotalPrice { get; set; }
    }

}
