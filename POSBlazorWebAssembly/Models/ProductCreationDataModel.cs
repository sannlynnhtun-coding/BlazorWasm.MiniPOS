namespace POSBlazorWebAssembly.Models
{
    public class ProductDataModel
    {
        public Guid product_id { get; set; } = Guid.NewGuid();
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string product_category_code { get; set; }
        public int product_sale_price { get; set; }
        public int product_buying_price { get; set; }
        public DateTime product_cration_date { get; set; } = DateTime.Now;
    }
}
