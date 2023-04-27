namespace POSBlazorWebAssembly.Models
{
    public class ProductCategoryDataModel
    {
        public Guid Product_category_id { get; set; } = Guid.NewGuid();
        public string Product_category_name { get; set; }
        public int Product_category_code { get; set; }
    }
}
