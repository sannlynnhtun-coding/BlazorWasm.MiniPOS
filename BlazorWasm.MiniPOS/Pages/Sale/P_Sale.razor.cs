using Microsoft.AspNetCore.Components;
using BlazorWasm.MiniPOS.Models;
using Stimulsoft.Report.Web;
using Stimulsoft.Report;

namespace BlazorWasm.MiniPOS.Pages.Sale
{
    public partial class P_Sale
    {
        private List<ProductNameListDataModel> lstProduct = new();
        private ProductSaleDataModel Model = new();
        private ProductSaleResponseDataModel lstProductSale = new();
        private int grand_total;
        private bool isEdit = false;
        protected override async Task OnInitializedAsync()
        {
            lstProduct = await db.GetProductNameList();
            //lstProductSale = await db.GetRecentProductSale();
            //grand_total = await db.GetGrandTotal();
        }

        async Task ProductNameChangeEvent(ChangeEventArgs e)
        {
            if (isEdit) return;
            if (e.Value.ToString() == "--Select Product Name--")
            {
                Model.product_price = 0;
            }
            else
            {
                Model.product_id = new Guid(e.Value.ToString());
                var lst = await db.GetProductName(Model.product_id);
                Model.product_price = lst.product_sale_price;
            }
        }

        async Task ProductQtyOnChangeEvent(ChangeEventArgs e)
        {
            ProductDataModel item = new();
            if (Model.product_id != null)
                item = await db.GetProductName(Model.product_id);
            Model.product_qty = Convert.ToInt32(e.Value.ToString());
            Model.product_price = item.product_sale_price;
            Model.product_name = string.IsNullOrEmpty(Model.product_name) ?
            item.product_name : Model.product_name;
            if (Model.product_price == 0)
            {
                Model.product_total_price = Model.product_qty * item.product_sale_price;
            }
            else
            {
                Model.product_total_price = Model.product_qty * Model.product_price;
            }
            grand_total = await db.GetGrandTotal();
        }

        async Task Add()
        {
            Model.product_id = Model.product_id;
            if (await db.CheckIsProductExit(Model.product_sale_id))
            {
                await db.UpdateProductSale(Model);
            }
            else
            {
                await db.SetSaleProduct(Model);
            }
            grand_total = await db.GetGrandTotal();
            lstProductSale = await db.GetRecentProductSale();
            Model = new();
        }

        async Task Save()
        {
            // lstProductSale = await db.GetRecentProductSale();
            lstProduct = new();
        }

        async Task DeleteProductSale(Guid guid)
        {
            await db.DeleteProductSale(guid);
            grand_total = await db.GetGrandTotal();
            lstProductSale = await db.GetRecentProductSale();
        }

        async Task EditProductSale(Guid guid)
        {
            isEdit = true;
            Model = await db.EditProductSale(guid);
            isEdit = false;
        }
    }
}
