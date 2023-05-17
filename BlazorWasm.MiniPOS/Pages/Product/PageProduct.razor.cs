using BlazorWasm.MiniPOS.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorWasm.MiniPOS.Pages.Product
{
    public partial class PageProduct
    {
        private List<ProductDataModel> lstProduct = new();
        private List<ProductCategoryDataModel> lstProductCategory = new();
        private ProductCategoryDataModel ProductCategory = new();
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
            lstProduct ??= new();
        }

        void Create()
        {
            Model = new();
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
            await ShowProductCategory(null,Model.product_category_code);
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

        async Task ProductNameChangeEvent(ChangeEventArgs e)
        {
            string str = e.Value.ToString();
            await ShowProductCategory(new Guid(str));
        }

        async Task ShowProductCategory(Guid? guid = null, string? productName = null)
        {
            lstProductCategory = await db.GetProductCategoryList();
            foreach (var item in lstProductCategory)
            {
                if (item.product_category_id == guid)
                {
                    Model.product_category_code = item.product_category_code;
                }
                else if(item.product_category_code == productName)
                {
                    ProductCategory.product_category_id = item.product_category_id;
                }
            }
        }
    }
}
