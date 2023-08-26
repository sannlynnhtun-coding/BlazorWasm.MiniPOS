using BlazorWasm.MiniPOS.Models;

namespace BlazorWasm.MiniPOS.Pages.ProductCategory
{
    public partial class PageProductCategory
    {
        private ProductCategoryResponseModel? _productCategoryResponseModel;
        private EnumFormType FormType { get; set; } = EnumFormType.List;
        private ProductCategoryDataModel _model = new();

        protected override async Task OnInitializedAsync()
        {
            await List();
        }

        async Task List(int pageNo = 1, int pageSize = 10)
        {
            var lst = await db.GetProductCategoryList();
            var pageCount = lst.Count / pageSize;
            if (lst.Count % pageSize > 0)
            {
                pageCount++;
            }

            var pageSetting = new PageSettingModel()
            {
                pageCount = pageCount,
                pageSize = pageSize,
                pageNo = pageNo
            };
            _productCategoryResponseModel = new ProductCategoryResponseModel()
            {
                productCategories = lst
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                pageSetting = pageSetting
            };
        }

        void Create()
        {
            FormType = EnumFormType.Create;
        }

        async Task Save()
        {
            await db.SetProductCategory(_model);
            _model = new();
            await List();
            FormType = EnumFormType.List;
        }

        async Task Edit(ProductCategoryDataModel product)
        {
            FormType = EnumFormType.Edit;
            _model = await db.GetProductCategory(product.product_category_id);
        }

        async Task Update()
        {
            await db.ProductCategoryUpdate(_model);
            _model = new ProductCategoryDataModel();
            await List();
            FormType = EnumFormType.List;
        }

        async Task Delete(ProductCategoryDataModel product)
        {
            await db.DeleteProductCategory(product.product_category_id);
            await List();
        }

        void Back()
        {
            FormType = EnumFormType.List;
        }
    }
}