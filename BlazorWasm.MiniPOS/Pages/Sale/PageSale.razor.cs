using Microsoft.AspNetCore.Components;
using BlazorWasm.MiniPOS.Models;
using TopFiveProducts = BlazorWasm.MiniPOS.Pages.Dashboard.Model.MonthlyTopFiveProductsOfCurrentYear;

namespace BlazorWasm.MiniPOS.Pages.Sale
{
    public partial class PageSale
    {
        private List<ProductDataModel>? _lstProduct = new();
        private ProductSaleDataModel _model = new();
        private ProductSaleResponseDataModel? _lstProductSale = new();
        private int _grandTotal;
        private bool _isEdit = false;
        private List<TopFiveProducts> _topFiveProducts = new();

        protected override async Task OnInitializedAsync()
        {
            _lstProduct = await db.GetProductList();
            _lstProduct ??= new List<ProductDataModel>();
            if (_lstProduct == null || !_lstProduct.Any())
                nav.NavigateTo("/setup/product-category");
            _lstProductSale = await db.GetRecentProductSale();
            _grandTotal = await db.GetGrandTotal();
        }

        private async Task ProductNameChange(object value)
        {
            if (_isEdit) return;
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
            _lstProduct = await db.GetProductList();
            _model = new ProductSaleDataModel();
        }

        private async Task Save()
        {
            await db.SetVoucher();
            _lstProduct = await db.GetProductList();
            _lstProductSale = await db.GetRecentProductSale();
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

        private async Task AddProductToSale(Guid productId)
        {
            var product = await db.GetProduct(productId);
            if (product == null) return;

            var existingSaleItem = await db.EditProductSale(productId);
            if (existingSaleItem != null && existingSaleItem.product_id == productId)
            {
                existingSaleItem.product_qty++;
                existingSaleItem.product_total_price = existingSaleItem.product_qty * existingSaleItem.product_price;
                await db.UpdateProductSale(existingSaleItem);
            }
            else
            {
                var newSaleItem = new ProductSaleDataModel
                {
                    product_id = product.product_id,
                    product_name = product.product_name,
                    product_price = product.product_sale_price,
                    product_qty = 1,
                    product_total_price = product.product_sale_price,
                    product_sale_id = product.product_id,
                    product_sale_date = DateTime.Now
                };
                await db.SetSaleProduct(newSaleItem);
            }

            _grandTotal = await db.GetGrandTotal();
            _lstProductSale = await db.GetRecentProductSale();
        }
    }
}