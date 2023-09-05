namespace BlazorWasm.MiniPOS.Pages.Reports.Charts
{
    public class YearOverYearModel
    {
        public string Year { get; set; }
        public List<long>? Data { get; set; }
    }
    public class YearOverYearReturnModel
    {
        public List<string> Year { get; set; } = new List<string>();
        public List<YearOverYearModel> YearData { get; set; } = new List<YearOverYearModel>();
    }
    public class YearOverYearResponseModel
    {
        public int Year { get; set; }
        public long TotalPrice { get; set; }
    }

    public class PastFiveYearModel
    {
        public string name { get; set; }
        public List<DataInfo> data { get; set; } = new();
    }

    public class DataInfo
    {
       public object[] array { get; set; }
    }

    public class DataReturnInfo
    {
        public object[][] arrayObject { get; set; }
    }
	public class DonutChartResponseModel
	{
		public List<DonutChartModel> data { get; set; }
	}
	public class DonutChartModel
	{
		public string name { get; set; }
		public int value { get; set; }
	}
	public static class JsonData
	{
		public static string str { get; } = @" {""data"":[
		{
			""name""  :""Norway"",
			""value"" :16
		},
		{
			""name""  :""Germany"",
			""value"" :12
		},
		{
			""name""  :""USA"",
			""value"" :8
		},
		{
			""name""  :""Sweden"",
			""value"" :8
		},
		{
			""name""  :""Netherlands"",
			""value"" :8
		},
		{
			""name""  :""ROC"",
			""value"" :6
		},
		{
			""name""  :""Austria"",
			""value"" :7
		},
		{
			""name""  :""Canada"",
			""value"" :4
		},
		{
			""name""  :""Japan"",
			""value"" :3
		}
	]
}";
    }
}
