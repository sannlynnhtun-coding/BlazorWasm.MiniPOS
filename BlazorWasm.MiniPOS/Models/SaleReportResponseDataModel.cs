namespace BlazorWasm.MiniPOS.Models
{
    public class SaleReportResponseDataModel
    {
        public List<SaleVoucherHeadDataModel> lstSaleReport { get; set; } = new List<SaleVoucherHeadDataModel>();
        public int TotalRowCount { get; set; }
        public int RowCount { get; set; }
        public int TotalPageNo { get; set; }
        public int CurrentPageNo { get; set; }
    }

    public class BestProductReportModel
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
    }
}
