using POSBlazorWebAssembly.Models;

namespace POSBlazorWebAssembly.Pages.ProductCategory
{
    public partial class P_ProductCategory
    {
        private EnumFormType FormType { get; set; } = EnumFormType.List;
        private List<ProductCategoryDataModel> lstProductCategory = new();
        private ProductCategoryDataModel Model = new();

        protected override async Task OnInitializedAsync()
        {
            await List();
        }

        async Task List()
        {
            lstProductCategory = await db.GetProductCategoryList();
        }

        void Create()
        {
            FormType = EnumFormType.Create;
        }

        async Task Save()
        {
            await db.SetProductCategory(Model);
            Model = new();
            await List();
            FormType = EnumFormType.List;
        }

        async Task Edit(ProductCategoryDataModel product)
        {
            FormType = EnumFormType.Edit;
            Model = await db.GetProductCategory(product.product_category_id);
        }

        async Task Update()
        {
            await db.ProductCategoryUpdate(Model);
            Model = new();
            await List();
            FormType = EnumFormType.List;
        }

        async Task Delete(ProductCategoryDataModel product)
        {
            await db.DeleteProductCategory(product.product_category_id);
            lstProductCategory = await db.GetProductCategoryList();
        }

        void Back()
        {
            FormType = EnumFormType.List;
        }
    }
}
