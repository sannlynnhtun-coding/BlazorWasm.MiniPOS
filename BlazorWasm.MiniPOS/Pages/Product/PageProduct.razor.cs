using BlazorWasm.MiniPOS.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWasm.MiniPOS.Pages.Product
{
    public partial class PageProduct
    {
        private ProductResponseModel? _productResponseModel;
        private List<ProductCategoryDataModel> _lstProductCategory = new();
        private ProductCategoryDataModel _productCategory = new ProductCategoryDataModel();
        private ProductDataModel _model = new();

        private EnumFormType FormType { get; set; } = EnumFormType.List;

        protected override async Task OnInitializedAsync()
        {
            await BindProductCategory();
            await List();
        }

        async Task BindProductCategory()
        {
            _lstProductCategory = await db.GetProductCategoryList();
        }

        async Task List(int pageNo = 1, int pageSize = 10)
        {
            var lst = await db.GetProductList();
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
            _productResponseModel = new ProductResponseModel()
            {
                products = lst
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                pageSetting = pageSetting
            };
        }

        void Create()
        {
            _model = new();
            FormType = EnumFormType.Create;
        }

        async Task Save()
        {
            await db.SetProduct(_model);
            _model = new();
            await List();
            FormType = EnumFormType.List;
        }

        async Task Edit(ProductDataModel product)
        {
            _model = await db.GetProduct(product.product_id);
            await ShowProductCategory(null, _model.product_category_code);
            FormType = EnumFormType.Edit;
        }

        async Task Update()
        {
            await db.ProductUpdate(_model);
            _model = new();
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

        async Task ProductNameChangeEvent(ChangeEventArgs e)
        {
            var str = e.Value?.ToString();
            if (str != null) await ShowProductCategory(new Guid(str));
        }

        async Task ShowProductCategory(Guid? guid = null, string? productName = null)
        {
            _lstProductCategory = await db.GetProductCategoryList();
            foreach (var item in _lstProductCategory)
            {
                if (item.product_category_id == guid)
                {
                    _model.product_category_code = item.product_category_code;
                }
                else if (item.product_category_code == productName)
                {
                    _productCategory.product_category_id = item.product_category_id;
                }
            }
        }
    }
}