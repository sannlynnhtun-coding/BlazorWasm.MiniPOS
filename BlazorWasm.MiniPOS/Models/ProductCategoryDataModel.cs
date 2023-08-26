namespace BlazorWasm.MiniPOS.Models
{
    public class ProductCategoryDataModel
    {
        public Guid product_category_id { get; set; } = Guid.NewGuid();
        public string product_category_name { get; set; }
        public string product_category_code { get; set; }
        public DateTime product_category_creation_date { get; set; } = DateTime.Now;
    }

    public class ProductCategoryResponseModel
    {
        public PageSettingModel pageSetting { get; set; }
        public List<ProductCategoryDataModel>? productCategories { get; set; }
    }
}
