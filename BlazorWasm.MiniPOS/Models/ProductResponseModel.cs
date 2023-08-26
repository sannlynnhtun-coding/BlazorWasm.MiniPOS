namespace BlazorWasm.MiniPOS.Models
{
    public class ProductResponseModel
    {
        public PageSettingModel pageSetting { get; set; }
        public List<ProductDataModel>? products { get; set; }
    }
}
