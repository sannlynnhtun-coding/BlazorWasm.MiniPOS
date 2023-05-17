using Blazored.LocalStorage;
using BlazorWasm.MiniPOS.Models;
using Microsoft.JSInterop;

namespace BlazorWasm.MiniPOS.Services
{
    public class LocalStorageService : IDbService
    {
        private readonly ILocalStorageService localStorage;
        public LocalStorageService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<List<ProductDataModel>> GetProductList()
        {
            //bool localStorageName = await JsRuntime.InvokeAsync<bool>("Tbl_Product");
            List<ProductDataModel> lst = await localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            return lst != null && lst.Count() > 1 ?
            lst.OrderByDescending(x => x.product_cration_date).ToList() : null;
        }

        public async Task SetProduct(ProductDataModel model)
        {
            List<ProductDataModel> lst = await localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            lst.Add(model);
            await localStorage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task<ProductDataModel> GetProduct(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            return lst.FirstOrDefault(x => x.product_id == guid);
        }

        public async Task ProductUpdate(ProductDataModel model)
        {
            //var lst = await GetProduct(model.product_id);
            var lst = await GetProductList();
            lst ??= new();
            var result = lst.FirstOrDefault(x => x.product_id == model.product_id);
            int index = lst.FindIndex(x => x.product_id == result.product_id);
            result.product_id = model.product_id;
            result.product_name = model.product_name;
            result.product_code = model.product_code;
            result.product_category_code = model.product_category_code;
            result.product_buying_price = model.product_buying_price;
            result.product_sale_price = model.product_sale_price;
            result.product_cration_date = model.product_cration_date;
            //lst.Add(result);
            lst[index] = result;
            await localStorage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task DeleteProduct(Guid guid)
        {
            var lst = await GetProductList();
            var item = lst.FirstOrDefault(x => x.product_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await localStorage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task<List<ProductCategoryDataModel>> GetProductCategoryList()
        {
            List<ProductCategoryDataModel> lst = await localStorage.GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
            lst ??= new();
            return lst.OrderByDescending(x => x.product_category_creation_date).ToList();
        }

        public async Task SetProductCategory(ProductCategoryDataModel model)
        {
            List<ProductCategoryDataModel> lst = await localStorage.GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
            lst ??= new();
            lst.Add(model);
            await localStorage.SetItemAsync("Tbl_ProductCategory", lst);
        }

        public async Task<ProductCategoryDataModel> GetProductCategory(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
            lst ??= new();
            return lst.FirstOrDefault(x => x.product_category_id == guid);
        }

        public async Task ProductCategoryUpdate(ProductCategoryDataModel model)
        {
            //var lst = await GetProduct(model.product_id);
            var lst = await GetProductCategoryList();
            lst ??= new();
            var result = lst.FirstOrDefault(x => x.product_category_id == model.product_category_id);
            int index = lst.FindIndex(x => x.product_category_id == result.product_category_id);
            result.product_category_id = model.product_category_id;
            result.product_category_name = model.product_category_name;
            result.product_category_code = model.product_category_code;
            result.product_category_creation_date = model.product_category_creation_date;
            //lst.Add(result);
            lst[index] = result;
            await localStorage.SetItemAsync("Tbl_ProductCategory", lst);
        }

        public async Task DeleteProductCategory(Guid guid)
        {
            var lst = await GetProductCategoryList();
            var item = lst.FirstOrDefault(x => x.product_category_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await localStorage.SetItemAsync("Tbl_ProductCategory", lst);
        }

        public async Task<List<ProductNameListDataModel>> GetProductNameList()
        {
            var lst = await GetProductList();
            lst ??= new();
            var lstProductName = lst.Select(x => new ProductNameListDataModel
            {
                product_id = x.product_id,
                product_name = x.product_name,
            }).ToList();
            lstProductName.Insert(0, new ProductNameListDataModel
            {
                product_id = null,
                product_name = "--Select Product Name--"
            });
            return lstProductName != null && lstProductName.Count() > 1 ? lstProductName : null;
                
        }

        public async Task<ProductDataModel> GetProductName(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            var result = lst.FirstOrDefault(x => x.product_id == guid);
            return result;
        }

        public async Task SetSaleProduct(ProductSaleDataModel model)
        {
            List<ProductSaleDataModel> lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            lst.Add(model);
            await localStorage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<int> GetGrandTotal()
        {
            List<ProductSaleDataModel> lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            return lst.Select(x => x.product_total_price).Sum();
        }

        public async Task<ProductSaleResponseDataModel> GetRecentProductSale(int pageNo = 1, int pageSize = 5)
        {
            var lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new List<ProductSaleDataModel>();
            int count = lst.Count();
            int totalPageNo = count / pageSize;
            if (count % pageSize > 0)
                totalPageNo++;
            var result = lst.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            return new ProductSaleResponseDataModel
            {
                lstProductSale = result,
                TotalPageNo = totalPageNo,
                RowCount = pageSize,
                TotalRowCount = count,
                CurrentPageNo = 1,
            };
        }

        public async Task<ProductSaleResponseDataModel> ProductSalePagination(int pageNo, int pageSize)
        {
            var lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var count = lst.Count;
            int totalPageNo = count / pageSize;
            int result = count % pageSize;
            if (result > 0)
                totalPageNo++;
            return new ProductSaleResponseDataModel
            {
                CurrentPageNo = pageNo,
                lstProductSale = lst.ToPage(pageNo, pageSize),
                RowCount = pageSize,
                TotalPageNo = totalPageNo,
                TotalRowCount = count
            };
        }

        public async Task DeleteProductSale(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_sale_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await localStorage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<ProductSaleDataModel> EditProductSale(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_sale_id == guid);
            if (item == null) return new ProductSaleDataModel();
            return item;
        }

        public async Task<bool> CheckIsProductExit(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_sale_id == guid);
            return item != null;
        }

        public async Task UpdateProductSale(ProductSaleDataModel model)
        {
            var lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var result = lst.FirstOrDefault(x => x.product_sale_id == model.product_sale_id);
            int index = lst.FindIndex(x => x.product_sale_id == result.product_sale_id);
            result.product_sale_id = model.product_sale_id;
            result.product_price = model.product_price;
            result.product_name = model.product_name;
            result.product_qty = model.product_qty;
            result.product_total_price = model.product_total_price;
            //lst.Add(result);
            lst[index] = result;
            await localStorage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<SaleReportResponseDataModel> SaleReport(DateTime dateTime)
        {
            var lst = await localStorage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new();
            DateTime searchDate = Convert.ToDateTime(dateTime.ToString("MM/dd/yyyy"));
            //DateTime searchDate = dateTime;
            var saleReport = lst.Where(x =>
                        Convert.ToDateTime(x.sale_date.ToString("MM/dd/yyyy")) == searchDate).ToList();
            int count = saleReport.Count();
            int rowCount = 5;
            int totalPageNo = count / rowCount;
            int result = count % rowCount;
            if (result > 0)
                totalPageNo++;
            return new SaleReportResponseDataModel
            {
                lstSaleReport = saleReport,
                TotalPageNo = totalPageNo,
                RowCount = rowCount,
                TotalRowCount = count,
                CurrentPageNo = 1,
            };
        }

        public async Task<SaleReportResponseDataModel> SaleReportPagination(int pageNo, int pageSize, DateTime dateTime)
        {
            var lst = await localStorage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new();
            DateTime searchDate = Convert.ToDateTime(dateTime.ToString("MM/dd/yyyy"));
            var saleReport = lst.Where(x => 
            Convert.ToDateTime(x.sale_date.ToString("MM/dd/yyyy")) == searchDate).ToList();
            int count = saleReport.Count();
            int totalPageNo = count / pageSize;
            int result = count % pageSize;
            if (result > 0)
                totalPageNo++;
            return new SaleReportResponseDataModel
            {
                CurrentPageNo = pageNo,
                lstSaleReport = saleReport.ToPage(pageNo, pageSize),
                RowCount = pageSize,
                TotalPageNo = totalPageNo,
                TotalRowCount = count
            };
        }

        public async Task<List<BestProductReportModel>> BestProductReport()
        {
            //var lst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            var lst = await localStorage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            var lstProduct = await localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            lstProduct ??= new();

            //var groupProducts = lst.Select(x => x.product_id).Distinct().ToList();
            var groupProducts = lst.Select(x => x.product_name).Distinct().ToList();

            List<BestProductReportModel> lstBestProductReport = new();
            foreach (var productId in groupProducts)
            {
                //int totalQty = lst.Where(x=> x.product_id==productId).Sum(x => x.product_qty);
                int totalQty = lst.Where(x=> x.product_name==productId).Sum(x => x.product_qty);
                lstBestProductReport.Add(new BestProductReportModel()
                {
                    //ProductName = lstProduct.FirstOrDefault(x=> x.product_id == productId)?.product_name,
                    ProductName = lstProduct.FirstOrDefault(x => x.product_name == productId)?.product_name,
                    ProductQuantity = totalQty
                });
            }

            return lstBestProductReport;
        }

        public async Task SetSaleVoucherDetail(SaleVoucherDetailDataModel model)
        {
            List<SaleVoucherDetailDataModel> lst = await localStorage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new();
            lst.Add(model);
            await localStorage.SetItemAsync("Tbl_SaleVoucherDetail", lst);
        }

        public async Task SetSaleVoucherHead(SaleVoucherHeadDataModel model)
        {
            List<SaleVoucherHeadDataModel> lst = await localStorage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new();
            lst.Add(model);
            await localStorage.SetItemAsync("Tbl_SaleVoucherHead", lst);
        }
        public async Task SetVouncher()
        {
            //Guid sale_voucher_detail_id = Guid.NewGuid();
            Guid sale_voucher_head_id = Guid.NewGuid();
            SaleVoucherDetailDataModel deatilModel = new();
            SaleVoucherHeadDataModel headModel = new();

            var getLst = await localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            getLst ??= new();
            if(getLst != null && getLst.Count() != 0)
            {
                foreach (var item in getLst)
                {
                    deatilModel.sale_voucher_detail_id = Guid.NewGuid();
                    deatilModel.product_price = item.product_price;
                    deatilModel.product_qty = item.product_qty;
                    deatilModel.product_name = item.product_name;
                    deatilModel.sale_voucher_head_id = sale_voucher_head_id;
                    deatilModel.detail_date = DateTime.Now;
                    await SetSaleVoucherDetail(deatilModel);
                }

                headModel.sale_voucher_head_id = sale_voucher_head_id;
                headModel.sale_total_amount = getLst.Select(x => x.product_total_price).Sum();
                headModel.sale_date = DateTime.Now;
                headModel.sale_voucher_no = Guid.NewGuid();
                await SetSaleVoucherHead(headModel);
                await localStorage.RemoveItemAsync("Tbl_ProductSale");
            }
        }

        public async Task<List<SaleVoucherDetailDataModel>> GetVoucherDetail(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new();
            var result = lst.Where(x=> x.sale_voucher_head_id == guid).ToList();
            return result;
        }
    }
}
