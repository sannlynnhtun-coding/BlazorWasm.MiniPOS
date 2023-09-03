using BlazorWasm.MiniPOS.Models;

namespace BlazorWasm.MiniPOS.Pages.GenerateData;

public partial class GenerateData
{
    private DateTime _startDate = DateTime.Now;
    private DateTime _endDate = DateTime.Now.AddYears(-5);
    private List<ProductNameListDataModel>? _lstProduct = new();
    private ProductSaleDataModel _model = new();
    private async Task GenerateDataByDate()
    {
        _lstProduct = await db.GetProductNameList();
        _lstProduct ??= new List<ProductNameListDataModel>();
        
        while (_startDate >= _endDate)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Random random = new Random();
                    
                    int quantity = random.Next(1, 11);
                    int randomIndex = random.Next(0, _lstProduct.Count);
                    ProductNameListDataModel selectedProduct = _lstProduct[randomIndex];
                    var item = await db.GetProductName(selectedProduct.product_id.Value);
                    if (item is not null)
                        _model.product_price = item.product_sale_price;
                    
                    _model.product_id = selectedProduct.product_id.Value;
                    _model.product_name = selectedProduct.product_name;
                    _model.product_qty = quantity;
                    _model.product_total_price = quantity * item.product_sale_price;
                    
                    if (await db.CheckIsProductExit(_model.product_sale_id))
                    {
                        await db.UpdateProductSale(_model);
                    }
                    else
                    {
                        await db.SetSaleProduct(_model);
                    }
                }
            }
            _startDate = _startDate.AddDays(-1);
        }
    }
}