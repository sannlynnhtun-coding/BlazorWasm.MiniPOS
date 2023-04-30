namespace POSBlazorWebAssembly.Models
{
    public class ProductSaleDataModel
    {
        public Guid product_sale_id { get; set; } = Guid.NewGuid();
        public string product_name { get; set; }
        public int product_qty { get; set; }
        public int product_price { get; set; }
        public int product_total_price { get; set; }
    }
}
