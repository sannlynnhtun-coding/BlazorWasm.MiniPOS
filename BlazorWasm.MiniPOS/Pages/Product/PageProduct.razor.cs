using BlazorWasm.MiniPOS.Models;

namespace BlazorWasm.MiniPOS.Pages.Product
{
    public partial class PageProduct
    {
        private List<ProductDataModel> lstProduct = new();
        private List<ProductCategoryDataModel> lstProductCategory = new();
        private ProductDataModel Model = new();
        private EnumFormType FormType { get; set; } = EnumFormType.List;

        protected override async Task OnInitializedAsync()
        {
            await BindProductCategory();
            await List();
        }

        async Task BindProductCategory()
        {
            lstProductCategory = await db.GetProductCategoryList();
        }

        async Task List()
        {
            lstProduct = await db.GetProductList();
        }

        void Create()
        {
            FormType = EnumFormType.Create;
        }

        async Task Save()
        {
            await db.SetProduct(Model);
            Model = new();
            await List();
            FormType = EnumFormType.List;
        }

        async Task Edit(ProductDataModel product)
        {
            Model = await db.GetProduct(product.product_id);
            FormType = EnumFormType.Edit;
        }

        async Task Update()
        {
            await db.ProductUpdate(Model);
            Model = new();
            await List();
            FormType = EnumFormType.List;
        }

        async Task Delete(ProductDataModel product)
        {
            await db.DeleteProduct(product.product_id);
            await List();
            FormType = EnumFormType.List;
        }

        void Back()
        {
            FormType = EnumFormType.List;
        }
    }
}
