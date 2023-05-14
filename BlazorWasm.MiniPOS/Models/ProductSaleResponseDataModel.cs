namespace BlazorWasm.MiniPOS.Models
{
    public class ProductSaleResponseDataModel
    {
        public List<ProductSaleDataModel> lstProductSale { get; set; } = new List<ProductSaleDataModel>();  
        public int TotalRowCount { get; set; }
        public int RowCount { get; set; }
        public int TotalPageNo { get; set; }
        public int CurrentPageNo { get; set; }
    }
}
