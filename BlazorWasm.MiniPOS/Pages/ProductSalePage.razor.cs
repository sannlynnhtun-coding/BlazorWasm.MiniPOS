using Microsoft.AspNetCore.Components;
using BlazorWasm.MiniPOS.Models;
using Stimulsoft.Report.Web;
using Stimulsoft.Report;

namespace BlazorWasm.MiniPOS.Pages
{
    public partial class ProductSalePage
    {
        private List<ProductNameListDataModel> lstProduct = new();
        private ProductSaleDataModel Model = new();
        private ProductSaleResponseDataModel lstProductSale = new();
        private int showReport = 0;
        private StiReport Report;
        //private string product_id;
        //private string product_qty;
        //private int product_price = 0;
        //private string product_name = string.Empty;
        private bool isEdit = false;
        private int grand_total;
        protected override async Task OnInitializedAsync()
        {
            lstProduct = await db.GetProductNameList();
            lstProductSale = await db.GetRecentProductSale();
            grand_total = await db.GetGrandTotal();
            //await PrintReport();
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

            //Model.product_id = new Guid(e.Value.ToString());
            ////var item =  db.GetProduct(new Guid(product_id));
            //if (Model.product_id != null)
            //{
            //    var lst = await db.GetProductName(Model.product_id);
            //    Model.product_price = lst.product_sale_price;
            //}
            //else
            //{
            //    Model.product_price = 0;
            //}
        }

        //async Task ProdcutQtyKeyPressEvent(KeyboardEventArgs e)
        //{
        //    int price = await db.GetProductName(new Guid(product_id));
        //    product_price = e.Key.ToString();
        //    product_price = string.IsNullOrEmpty(product_price) ? "0" : product_price; 
        //    //if (string.IsNullOrEmpty(product_price))
        //    //    product_price = "0";
        //    Model.product_qty = Convert.ToInt32(product_price);
        //    Model.product_total_price = Model.product_qty * price;
        //}

        async Task ProductQtyOnChangeEvent(ChangeEventArgs e)
        {
            ProductDataModel item = new();
            if (Model.product_id != null)
                item = await db.GetProductName(Model.product_id);
            Model.product_qty = Convert.ToInt32(e.Value.ToString());
            Model.product_price = item.product_sale_price;
            //Model.product_qty = string.IsNullOrEmpty(Model.product_qty) ? "0" : Model.product_qty;
            //if (string.IsNullOrEmpty(product_price))
            //    product_price = "0";
            //Model.product_name = null ? lst.product_name : product_name;
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
            showReport = 1;
            //await db.SetVouncher();
            await PrintReport();
            lstProductSale = await db.GetRecentProductSale();
        }

        async Task PrintReport()
        {
            //How to activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //var licenseStream = await Http.GetStreamAsync("Secret/license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(licenseStream);

            //Create empty report object
            this.Report = new StiReport();

            //Load report template
            var reportBytes = await Http.GetByteArrayAsync("Reports/BusinessObjectsCopy.mrt");
            this.Report.Load(reportBytes);

            //Create business object
            var list = await GetBusinessObject();

            //Register business object in the report template
            this.Report.RegBusinessObject("MyList", list);
            this.Report.Dictionary.Synchronize();
        }

        private async void OnSaveReport(StiSaveReportEventArgs args)
        {
            var reportJson = args.Report.SaveToJsonString();

            await Task.CompletedTask;
        }

        private async Task<System.Collections.ArrayList> GetBusinessObject()
        {
            //var lst = await db.GetProductSale();
            var list = new System.Collections.ArrayList();
            //foreach (var item in lst)
            //{
            //    list.Add(new BusinessEntity(item.product_name, item.product_price.ToString("n2"),
            //    item.product_qty.ToString("n2"), item.product_total_price.ToString("n2")));
            //}
            ////list.Add(new BusinessEntity("name11", "alias1"));
            ////list.Add(new BusinessEntity("name22", "alias2"));
            ////list.Add(new BusinessEntity("name33", "alias3"));

            return list;
        }

        async Task DeleteProductSale(Guid guid)
        {
            await db.DeleteProductSale(guid);
            //await db.GetRecentExpenses();
            grand_total = await db.GetGrandTotal();
            lstProductSale = await db.GetRecentProductSale();
        }

        async Task EditProductSale(Guid guid)
        {
            isEdit = true;
            Model = await db.EditProductSale(guid);
            //await db.GetRecentExpenses();
            isEdit = false;
        }
    }
}
