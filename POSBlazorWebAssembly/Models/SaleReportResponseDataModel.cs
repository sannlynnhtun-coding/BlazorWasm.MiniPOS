namespace POSBlazorWebAssembly.Models
{
    public class SaleReportResponseDataModel
    {
        public List<ProductSaleDataModel> lstSaleReport { get; set; } = new List<ProductSaleDataModel>();
        public int TotalRowCount { get; set; }
        public int RowCount { get; set; }
        public int TotalPageNo { get; set; }
        public int CurrentPageNo { get; set; }
    }
}
