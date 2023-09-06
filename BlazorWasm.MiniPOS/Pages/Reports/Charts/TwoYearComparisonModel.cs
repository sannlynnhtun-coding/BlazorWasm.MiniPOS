namespace BlazorWasm.MiniPOS.Pages.Reports.Charts
{
    public class TwoYearComparisonModel
    {
        public string name { get; set; }
        public string color { get; set; }
        public List<double> data { get; set; } = new();
        public double pointPadding { get; set; }
        public double pointPlacement { get; set; }
        public ChartTooltip tooltip { get; set; } = new();
        public int? yAxis { get; set; }
    }

    public class ChartTooltip
    {
        public string valuePrefix { get; set; }
        public string valueSuffix { get; set; }
    }

}
