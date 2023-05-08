namespace POSBlazorWebAssembly.Models
{
    public class ProductSaleDataModel
    {
        public Guid product_sale_id { get; set; } = Guid.NewGuid();
        public Guid product_id { get; set; }
        public string product_name { get; set; }
        public int product_qty { get; set; }
        public int product_price { get; set; }
        public int product_total_price { get; set; }
        public DateTime product_sale_date { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
    }
}
