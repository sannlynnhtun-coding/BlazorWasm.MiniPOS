using POSBlazorWebAssembly.Models;

namespace POSBlazorWebAssembly.Pages.Product
{
    public partial class P_Product
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
        }

        async Task Edit(ProductDataModel product)
        {
            Model = await db.GetProduct(product.product_id);
        }

        async Task Update()
        {
            await db.ProductUpdate(Model);
            Model = new();
            await List();
        }

        async Task Delete(ProductDataModel product)
        {
            await db.DeleteProduct(product.product_id);
            lstProduct = await db.GetProductList();
        }

        void Back()
        {
            FormType = EnumFormType.List;
        }
    }
}
