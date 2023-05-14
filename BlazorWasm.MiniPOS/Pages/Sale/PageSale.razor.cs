using Microsoft.AspNetCore.Components;
using BlazorWasm.MiniPOS.Models;
using Stimulsoft.Report.Web;
using Stimulsoft.Report;

namespace BlazorWasm.MiniPOS.Pages.Sale
{
    public partial class PageSale
    {
        private List<ProductNameListDataModel> _lstProduct = new();
        private ProductSaleDataModel _model = new();
        private ProductSaleResponseDataModel _lstProductSale = new();
        private int _grandTotal;
        private bool _isEdit = false;
        protected override async Task OnInitializedAsync()
        {
            _lstProduct = await db.GetProductNameList();
            //lstProductSale = await db.GetRecentProductSale();
            //grand_total = await db.GetGrandTotal();
        }

        private async Task ProductNameChange(ChangeEventArgs e)
        {
            if (_isEdit) return;
            if (e.Value?.ToString() == "--Select Product Name--")
            {
                _model.product_price = 0;
            }
            else
            {
                _model.product_id = new Guid(e.Value.ToString() ?? string.Empty);
                var lst = await db.GetProductName(_model.product_id);
                _model.product_price = lst.product_sale_price;
            }
        }

        private async Task ProductQtyOnChangeEvent(ChangeEventArgs e)
        {
            ProductDataModel item = new();
            item = await db.GetProductName(_model.product_id);
            _model.product_qty = Convert.ToInt32(e.Value.ToString());
            _model.product_price = item.product_sale_price;
            _model.product_name = string.IsNullOrEmpty(_model.product_name) ?
            item.product_name : _model.product_name;
            if (_model.product_price == 0)
            {
                _model.product_total_price = _model.product_qty * item.product_sale_price;
            }
            else
            {
                _model.product_total_price = _model.product_qty * _model.product_price;
            }
            _grandTotal = await db.GetGrandTotal();
        }

        private async Task Add()
        {
            _model.product_id = _model.product_id;
            if (await db.CheckIsProductExit(_model.product_sale_id))
            {
                await db.UpdateProductSale(_model);
            }
            else
            {
                await db.SetSaleProduct(_model);
            }
            _grandTotal = await db.GetGrandTotal();
            _lstProductSale = await db.GetRecentProductSale();
            _model = new();
        }

        private Task Save()
        {
            // lstProductSale = await db.GetRecentProductSale();
            _lstProduct = new();
            return Task.CompletedTask;
        }

        private async Task DeleteProductSale(Guid guid)
        {
            await db.DeleteProductSale(guid);
            _grandTotal = await db.GetGrandTotal();
            _lstProductSale = await db.GetRecentProductSale();
        }

        private async Task EditProductSale(Guid guid)
        {
            _isEdit = true;
            _model = await db.EditProductSale(guid);
            _isEdit = false;
        }
    }
}
