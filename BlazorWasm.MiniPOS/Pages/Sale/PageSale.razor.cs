using Microsoft.AspNetCore.Components;
using BlazorWasm.MiniPOS.Models;
using TopFiveProducts = BlazorWasm.MiniPOS.Pages.Reports.Charts.MonthlyTopFiveProductsOfCurrentYear;

namespace BlazorWasm.MiniPOS.Pages.Sale
{
    public partial class PageSale
    {
        private List<ProductNameListDataModel>? _lstProduct = new();
        private ProductSaleDataModel _model = new();
        private ProductSaleResponseDataModel? _lstProductSale = new();
        private int _grandTotal;
        private bool _isEdit = false;
        private List<TopFiveProducts> _topFiveProducts = new();

        protected override async Task OnInitializedAsync()
        {
            _lstProduct = await db.GetProductNameList();
            _lstProduct ??= new List<ProductNameListDataModel>();
            if (_lstProduct == null || !_lstProduct.Any())
                nav.NavigateTo("/setup/product-category");
            _lstProductSale = await db.GetRecentProductSale();
            _grandTotal = await db.GetGrandTotal();
        }

        // private async Task ProductNameChange(ChangeEventArgs e)
        private async Task ProductNameChange(object value)
        {
            if (_isEdit) return;
            // if (e.Value?.ToString() == "--Select Product Name--")
            // {
            //     _model.product_price = 0;
            // }
            // else
            // {
            //     _model.product_id = new Guid(e.Value.ToString() ?? string.Empty);
            //     var lst = await db.GetProductName(_model.product_id);
            //     _model.product_price = lst.product_sale_price;
            // }
            if (value is Guid productId)
            {
                _model.product_id = productId;
                var item = await db.GetProductName(_model.product_id);
                if (item is not null)
                    _model.product_price = item.product_sale_price;
            }
        }

        private async Task ProductQtyOnChangeEvent(ChangeEventArgs e)
        {
            ProductDataModel item = new();
            item = await db.GetProductName(_model.product_id);
            _model.product_qty = Convert.ToInt32(e.Value.ToString());
            _model.product_price = item.product_sale_price;
            _model.product_name = string.IsNullOrEmpty(_model.product_name) ? item.product_name : _model.product_name;
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
            if (await db.CheckIsProductExit(_model.product_id))
            {
                await db.UpdateProductSale(_model);
            }
            else
            {
                await db.SetSaleProduct(_model);
            }

            _grandTotal = await db.GetGrandTotal();
            _lstProductSale = await db.GetRecentProductSale();
            _lstProduct = await db.GetProductNameList();
            _model = new ProductSaleDataModel();
        }

        private async Task Save()
        {
            await db.SetVoucher();
            _lstProduct = await db.GetProductNameList();
            _lstProductSale = await db.GetRecentProductSale();
            //_lstProduct = new();
            //return Task.CompletedTask;
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