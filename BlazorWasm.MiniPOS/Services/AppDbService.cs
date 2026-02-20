using System.Globalization;
using System.Text.Json;
using Blazored.LocalStorage;
using BlazorWasm.MiniPOS.Models;
using TopFiveProducts = BlazorWasm.MiniPOS.Pages.Dashboard.Model.MonthlyTopFiveProductsOfCurrentYear;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System;
using BlazorWasm.MiniPOS.Pages.Dashboard.Model;

namespace BlazorWasm.MiniPOS.Services
{
    public class AppDbService : IDbService
    {
        private readonly IStorageService _storageService;
        private IStorageProvider _storage => _storageService.CurrentProvider;

        public AppDbService(IStorageService storageService)
        {
            this._storageService = storageService;
        }

        public async Task<List<ProductDataModel>> GetProductList()
        {
            //bool localStorageName = await JsRuntime.InvokeAsync<bool>("Tbl_Product");
            var lst = await _storage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            if (lst.Count != 0)
                return lst.Any()
                    ? lst.OrderByDescending(x => x.product_creation_date).ToList()
                    : new List<ProductDataModel>();
            var r = new Random();
            var count = 0;
            foreach (var item in GetProduct().Distinct())
            {
                count++;
                var product = new ProductDataModel()
                {
                    //product_buying_price = r.Next(1000, 100000),
                    product_buying_price = r.Next(10, 100),
                    product_category_code = "PC0001",
                    product_code = "P" + count.ToString().PadLeft(4, '0'),
                    product_creation_date = DateTime.Now,
                    product_id = Guid.NewGuid(),
                    product_name = item,
                };
                product.product_sale_price =
                    r.Next(product.product_buying_price, product.product_buying_price * 100);
                lst.Add(product);
            }

            await _storage.SetItemAsync("Tbl_Product", lst);

            return lst.Any()
                ? lst.OrderByDescending(x => x.product_creation_date).ToList()
                : new List<ProductDataModel>();
        }

        public async Task SetProduct(ProductDataModel model)
        {
            var lst = await _storage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            lst.Add(model);
            await _storage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task<ProductDataModel> GetProduct(Guid guid)
        {
            var lst = await _storage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            return lst.FirstOrDefault(x => x.product_id == guid) ?? throw new InvalidOperationException();
        }

        public async Task ProductUpdate(ProductDataModel model)
        {
            //var lst = await GetProduct(model.product_id);
            var lst = await GetProductList();
            var result = lst.FirstOrDefault(x => x.product_id == model.product_id);
            var index = lst.FindIndex(x => result != null && x.product_id == result.product_id);
            if (result != null)
            {
                result.product_id = model.product_id;
                result.product_name = model.product_name;
                result.product_code = model.product_code;
                result.product_category_code = model.product_category_code;
                result.product_buying_price = model.product_buying_price;
                result.product_sale_price = model.product_sale_price;
                result.product_creation_date = model.product_creation_date;
                //lst.Add(result);
                lst[index] = result;
            }

            await _storage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task DeleteProduct(Guid guid)
        {
            var lst = await GetProductList();
            var item = lst.FirstOrDefault(x => x.product_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await _storage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task<List<ProductCategoryDataModel>> GetProductCategoryList()
        {
            var lst = await _storage
                .GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
            lst ??= new List<ProductCategoryDataModel>();
            if (lst.Count != 0)
                return lst
                    .OrderByDescending(x => x.product_category_creation_date)
                    .ThenByDescending(x => x.product_category_code)
                    .ToList();
            var count = 0;
            foreach (var item in GetProductCategory())
            {
                count++;
                lst.Add(new ProductCategoryDataModel
                {
                    product_category_code = "PC" + count.ToString().PadLeft(4, '0'),
                    product_category_creation_date = DateTime.Now,
                    product_category_id = Guid.NewGuid(),
                    product_category_name = item
                });
            }

            await _storage.SetItemAsync("Tbl_ProductCategory", lst);
            return lst
                .OrderByDescending(x => x.product_category_creation_date)
                .ThenByDescending(x => x.product_category_code)
                .ToList();
        }

        public async Task SetProductCategory(ProductCategoryDataModel model)
        {
            var lst = await _storage.GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
            lst ??= new();
            lst.Add(model);
            await _storage.SetItemAsync("Tbl_ProductCategory", lst);
        }

        public async Task<ProductCategoryDataModel> GetProductCategory(Guid guid)
        {
            var lst = await _storage.GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
            lst ??= new List<ProductCategoryDataModel>();
            return lst.FirstOrDefault(x => x.product_category_id == guid) ?? throw new InvalidOperationException();
        }

        public async Task ProductCategoryUpdate(ProductCategoryDataModel model)
        {
            //var lst = await GetProduct(model.product_id);
            var lst = await GetProductCategoryList();
            var result = lst.FirstOrDefault(x => x.product_category_id == model.product_category_id);
            var index = lst.FindIndex(x => x.product_category_id == result.product_category_id);
            if (result != null)
            {
                result.product_category_id = model.product_category_id;
                result.product_category_name = model.product_category_name;
                result.product_category_code = model.product_category_code;
                result.product_category_creation_date = model.product_category_creation_date;
                //lst.Add(result);
                lst[index] = result;
            }

            await _storage.SetItemAsync("Tbl_ProductCategory", lst);
        }

        public async Task DeleteProductCategory(Guid guid)
        {
            var lst = await GetProductCategoryList();
            var item = lst.FirstOrDefault(x => x.product_category_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await _storage.SetItemAsync("Tbl_ProductCategory", lst);
        }

        public async Task<List<ProductNameListDataModel>> GetProductNameList()
        {
            var lst = await GetProductList();
            var lstProductName = lst
                .Select(x => new ProductNameListDataModel
                {
                    product_id = x.product_id,
                    product_name = x.product_name,
                })
                .OrderBy(x => x.product_name)
                .ToList();
            // lstProductName.Insert(0, new ProductNameListDataModel
            // {
            //     product_id = null,
            //     product_name = "--Select Product Name--"
            // });
            return (lstProductName.Count > 1 ? lstProductName : null) ?? throw new InvalidOperationException();
        }

        public async Task<ProductDataModel?> GetProductName(Guid guid)
        {
            var lst = await _storage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new List<ProductDataModel>();
            var result = lst.FirstOrDefault(x => x.product_id == guid);
            return result ?? throw new InvalidOperationException();
        }

        public async Task<ProductDataModel?> GetProductByName(String name)
        {
            var lst = await _storage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new List<ProductDataModel>();
            var result = lst.FirstOrDefault(x => x.product_name == name);
            return result ?? throw new InvalidOperationException();
        }

        public async Task SetSaleProduct(ProductSaleDataModel model)
        {
            var lst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            lst.Add(model);
            await _storage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<int> GetGrandTotal()
        {
            var lst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            return lst.Select(x => x.product_total_price).Sum();
        }

        public async Task<ProductSaleResponseDataModel> GetRecentProductSale(int pageNo = 1, int pageSize = 5)
        {
            var lst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new List<ProductSaleDataModel>();
            var count = lst.Count();
            var totalPageNo = count / pageSize;
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
            var lst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var count = lst.Count;
            var totalPageNo = count / pageSize;
            var result = count % pageSize;
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
            var lst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_sale_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await _storage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<ProductSaleDataModel> EditProductSale(Guid guid)
        {
            var lst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_sale_id == guid);
            if (item == null) return new ProductSaleDataModel();
            return item;
        }

        public async Task<bool> CheckIsProductExit(Guid guid)
        {
            var lst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_id == guid);
            return item != null;
        }

        public async Task UpdateProductSale(ProductSaleDataModel model)
        {
            var lst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var result = lst.FirstOrDefault(x => x.product_sale_id == model.product_sale_id);
            var index = lst.FindIndex(x => x.product_sale_id == result?.product_sale_id);
            if (result != null)
            {
                result.product_sale_id = model.product_sale_id;
                result.product_price = model.product_price;
                result.product_id = model.product_id;
                result.product_name = model.product_name;
                result.product_qty = model.product_qty;
                result.product_total_price = model.product_total_price;
                //lst.Add(result);
                lst[index] = result;
            }

            await _storage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<SaleReportResponseDataModel> SaleReport(DateTime dateTime)
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new();
            var searchDate = Convert.ToDateTime(dateTime.ToString("MM/dd/yyyy"));
            //DateTime searchDate = dateTime;
            var saleReport = lst.Where(x =>
                Convert.ToDateTime(x.sale_date.ToString("MM/dd/yyyy")) == searchDate).ToList();
            var count = saleReport.Count();
            var rowCount = 5;
            var totalPageNo = count / rowCount;
            var result = count % rowCount;
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
            var lst = await _storage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new();
            var searchDate = Convert.ToDateTime(dateTime.ToString("MM/dd/yyyy"));
            var saleReport = lst.Where(x =>
                Convert.ToDateTime(x.sale_date.ToString("MM/dd/yyyy")) == searchDate).ToList();
            var count = saleReport.Count();
            var totalPageNo = count / pageSize;
            var result = count % pageSize;
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
            var lst = await _storage
                .GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            var lstProduct = await _storage
                .GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            lstProduct ??= new();

            //var groupProducts = lst.Select(x => x.product_id).Distinct().ToList();
            var groupProducts = lst.Select(x => x.product_id)
                .Distinct()
                .ToList();

            List<BestProductReportModel> lstBestProductReport = new();
            foreach (var productId in groupProducts)
            {
                //int totalQty = lst.Where(x=> x.product_id==productId).Sum(x => x.product_qty);
                var totalQty = lst
                    .Where(x => x.product_id == productId)
                    .Sum(x => x.product_qty);
                lstBestProductReport.Add(new BestProductReportModel()
                {
                    //ProductName = lstProduct.FirstOrDefault(x=> x.product_id == productId)?.product_name,
                    ProductName = lstProduct
                                      .FirstOrDefault(
                                          x =>
                                              x.product_id == new Guid(productId))
                                      ?.product_name ??
                                  throw new InvalidOperationException(),
                    ProductQuantity = totalQty
                });
            }

            return lstBestProductReport;
        }

        private async Task SetSaleVoucherDetail(SaleVoucherDetailDataModel model)
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new List<SaleVoucherDetailDataModel>();
            lst.Add(model);
            await _storage.SetItemAsync("Tbl_SaleVoucherDetail", lst);
        }

        private async Task SetSaleVoucherHead(SaleVoucherHeadDataModel model)
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new List<SaleVoucherHeadDataModel>();
            lst.Add(model);
            await _storage.SetItemAsync("Tbl_SaleVoucherHead", lst);
        }

        public async Task SetVoucher()
        {
            //Guid sale_voucher_detail_id = Guid.NewGuid();
            var saleVoucherHeadId = Guid.NewGuid();
            SaleVoucherHeadDataModel headModel = new();
            DateTime voucherDate = DateTime.Now;

            var getLst = await _storage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            getLst ??= new();
            if (getLst.Count > 0)
            {
                foreach (var item in getLst)
                {
                    SaleVoucherDetailDataModel detail = new()
                    {
                        sale_voucher_detail_id = Guid.NewGuid(),
                        product_price = item.product_price,
                        product_qty = item.product_qty,
                        product_name = item.product_name,
                        product_id = item.product_id.ToString(),
                        sale_voucher_head_id = saleVoucherHeadId,
                        detail_date = item.product_sale_date
                    };
                    await SetSaleVoucherDetail(detail);
                    voucherDate = item.product_sale_date;
                }

                headModel.sale_voucher_head_id = saleVoucherHeadId;
                headModel.sale_total_amount = getLst.Select(x => x.product_total_price).Sum();
                headModel.sale_date = voucherDate;
                headModel.sale_voucher_no = Guid.NewGuid();
                await SetSaleVoucherHead(headModel);
                await _storage.RemoveItemAsync("Tbl_ProductSale");
            }
        }

        public async Task<List<SaleVoucherDetailDataModel>> GetVoucherDetail(Guid guid)
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new List<SaleVoucherDetailDataModel>();
            var result = lst.Where(x => x.sale_voucher_head_id == guid).ToList();
            return result;
        }

        public async Task<List<SaleVoucherHeadDataModel>> GetSaleVoucherHead()
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new List<SaleVoucherHeadDataModel>();
            return lst;
        }

        public async Task<YearOverYearReturnModel> YearOverYearChart(DateTime dateTime)
        {
            var year = dateTime.Year;
            var pastThreeYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <= year && x.sale_date.Year >= pastThreeYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new YearOverYearResponseModel
                {
                    Year = s.Key,
                    TotalPrice = s.Sum(sale => sale.sale_total_amount)
                }).ToList();

            YearOverYearReturnModel returnModel = new();
            List<YearOverYearModel> yearData = new();
            for (var i = 1; i < dataList.Count; i++)
            {
                returnModel.Year.Add(dataList[0].Year.ToString() + "/" +
                                     dataList[i].Year.ToString());

                yearData.Add(new YearOverYearModel
                {
                    Year = dataList[i].Year.ToString(),
                    Data = new List<long>
                        { Convert.ToInt64(dataList[0].TotalPrice), Convert.ToInt64(dataList[i].TotalPrice) }
                });
            }

            returnModel.YearData = yearData;
            return returnModel;
        }

        public async Task<List<TwoYearComparisonModel>> CompareTwoYear(int firstYear,
            int secondYear)
        {
            var lst = await GetSaleVoucherHead();
            var firstResultLst = lst
                .Where(x => x.sale_date.Year == firstYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new
                {
                    Year = s.Key,
                    Amount = s.Sum(sale => sale.sale_total_amount)
                }).ToList();
            var secondResultLst = lst
                .Where(x => x.sale_date.Year == secondYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new
                {
                    Year = s.Key,
                    Amount = s.Sum(sale => sale.sale_total_amount)
                }).ToList();
            List<TwoYearComparisonModel> returnData = new();
            if (firstResultLst.Count == 0 || secondResultLst.Count == 0)
                return returnData;
            foreach (var item in firstResultLst)
            {
                for (int i = 0; i < 2; i++)
                {
                    var result = GenerateColor();
                    returnData.Add(new TwoYearComparisonModel
                    {
                        name = i != 0 ? item.Year.ToString() + "Optimized" : item.Year.ToString(),
                        color = $"rgba({result.Item1},{result.Item2},{result.Item3},{1})",
                        data = new List<double>
                        {
                            i == 0 ? item.Amount / 10 : secondResultLst[0].Amount / 10,
                            i == 0 ? item.Amount / 100 : secondResultLst[0].Amount / 100,
                            i == 0 ? item.Amount / 1000 : secondResultLst[0].Amount / 1000,
                        },
                        pointPadding = i != 0 ? 0.4 : 0.3,
                        pointPlacement = -0.2,
                    });
                }
            }

            foreach (var item in secondResultLst)
            {
                for (int i = 0; i < 2; i++)
                {
                    var result = GenerateColor();
                    returnData.Add(new TwoYearComparisonModel
                    {
                        name = i != 0 ? item.Year.ToString() + "Optimized" : item.Year.ToString(),
                        color = $"rgba({result.Item1},{result.Item2},{result.Item3},{1})",
                        data = new List<double>
                        {
                            i == 0 ? item.Amount / 10 : firstResultLst[0].Amount / 10,
                            i == 0 ? item.Amount / 100 : firstResultLst[0].Amount / 100,
                            i == 0 ? item.Amount / 1000 : firstResultLst[0].Amount / 1000,
                        },
                        tooltip = new ChartTooltip
                        {
                            valuePrefix = "$",
                            valueSuffix = "M"
                        },
                        pointPadding = i != 0 ? 0.4 : 0.3,
                        pointPlacement = 0.2,
                        yAxis = 1
                    });
                }
            }

            return returnData;
        }

        public (int, int, int) GenerateColor()
        {
            Random random = new();
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);

            return (red, green, blue);
        }

        public async Task<YearlySaleAmountModel> YearlySaleAmount()
        {
            var year = DateTime.Now.Year;
            var pastFiveYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <= year && x.sale_date.Year >= pastFiveYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new
                {
                    Year = s.Key,
                    Amount = s.Sum(sale => sale.sale_total_amount)
                }).ToList();
            YearlySaleAmountModel returnModel = new();
            foreach (var item in dataList)
            {
                returnModel.data.Add(item.Amount);
                returnModel.category.Add(item.Year.ToString());
            }

            return returnModel;
        }

        public async Task<List<int>> GetYearList()
        {
            var year = DateTime.Now.Year;
            var pastFiveYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <= year && x.sale_date.Year >= pastFiveYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new
                {
                    Year = s.Key,
                }).ToList();
            List<int> yearList = new();
            foreach (var item in dataList)
            {
                yearList.Add(item.Year);
            }

            return yearList;
        }

        public async Task<PastFiveYearModel> PastFiveYearV1(DateTime date)
        {
            var year = date.Year;
            var pastFiveYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <= year && x.sale_date.Year >= pastFiveYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new
                {
                    Year = s.Key,
                    TotalPrice = s.Sum(sale => sale.sale_total_amount)
                }).ToList();
            PastFiveYearModel returnModel = new();
            List<DataInfo> data = new();
            returnModel.name = "TotalPrice";

            foreach (var item in dataList)
            {
                var dataInfo = new DataInfo
                {
                    array = new object[2]
                };
                dataInfo.array[0] = item.Year.ToString();
                dataInfo.array[1] = item.TotalPrice;

                data.Add(dataInfo);
            }

            returnModel.data = data;
            return returnModel;
        }

        public async Task<DonutChartResponseModel> PastFiveYear(DateTime date)
        {
            DonutChartResponseModel model = new DonutChartResponseModel();
            var year = date.Year;
            var pastFiveYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <= year && x.sale_date.Year >= pastFiveYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new
                {
                    Year = s.Key,
                    Amount = s.Sum(sale => sale.sale_total_amount)
                }).ToList();

            var totalPrice = dataList.Select(x => x.Amount).Sum();

            model.data = dataList.Select(x => new DonutChartModel
            {
                name = x.Year.ToString(),
                value = x.Amount
            }).ToList();

            return model;
        }

        public async Task<DataReturnInfo> PastFiveYearFunnelChart(DateTime date)
        {
            var year = date.Year;
            var pastFiveYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <= year && x.sale_date.Year >= pastFiveYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new
                {
                    Year = s.Key,
                    Amount = s.Sum(sale => sale.sale_total_amount)
                }).ToList();

            var totalPrice = dataList.Select(x => x.Amount).Sum();

            var data = new DataReturnInfo
            {
                arrayObject = new object[dataList.Count][]
            };
            for (int index = 0; index < dataList.Count; index++)
            {
                data.arrayObject[index] = new object[]
                {
                    dataList[index].Year.ToString(),
                    dataList[index].Amount
                };
            }

            return data;
        }

        public async Task<PastFiveYearsDailyModel> PastFiveYearsDailyAmount(DateTime dateTime)
        {
            var startDate = dateTime;
            var endDate = dateTime.AddYears(-5);
            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new List<SaleVoucherDetailDataModel>();
            var dataList = lst
                .Where(x => x.detail_date <= startDate
                            && x.detail_date >= endDate).ToList();
            int count = 0;
            int inner = 0;
            int plusFive = 5;
            var data = new PastFiveYearsDailyModel
            {
                dataArray = new string[dataList.Count][]
            };
            for (int i = 0; i < dataList.Count(); i++)
            {
                data.dataArray[i] = new string[]
                {
                    dataList[inner].product_name + dataList[inner].product_qty + " = " + dataList[inner].product_price,
                    dataList[count].product_name + dataList[count].product_qty + " = " + dataList[count].product_price
                };
                count++;
                count = count < dataList.Count ? count : dataList.Count - 1;
                if (count > plusFive)
                {
                    inner = inner + 1;
                    plusFive += 5;
                }
            }

            return data;
        }

        public async Task<DonutChartResponseModel> DonutChart()
        {
            var data = JsonConvert.DeserializeObject<DonutChartResponseModel>(JsonData.str);
            return await Task.FromResult(data);
        }

        public async Task GenerateYearOverYear()
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            if (lst is null)
            {
                DateTime _startDate = DateTime.Now;
                DateTime _endDate = DateTime.Now.AddYears(-5);
                ProductSaleDataModel _model = new();
                var _lstProduct = await GetProductNameList();
                _lstProduct ??= new List<ProductNameListDataModel>();
                while (_startDate >= _endDate)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Random random = new Random();
                            int quantity = random.Next(1, 11);
                            int randomIndex = random.Next(0, _lstProduct.Count);
                            ProductNameListDataModel selectedProduct = _lstProduct[randomIndex];
                            var item = await GetProductName(selectedProduct.product_id);
                            if (item is not null)
                            {
                                _model.product_price = item.product_sale_price;
                                _model.product_id = selectedProduct.product_id;
                                _model.product_name = selectedProduct.product_name;
                                _model.product_qty = quantity;
                                _model.product_total_price = quantity * item.product_sale_price;
                                _model.product_sale_date = _startDate;
                                _model.product_sale_id = Guid.NewGuid();
                            }

                            if (await CheckIsProductExit(_model.product_id))
                            {
                                await UpdateProductSale(_model);
                            }
                            else
                            {
                                await SetSaleProduct(_model);
                            }
                        }

                        await SetVoucher();
                    }

                    _startDate = _startDate.AddYears(-1);
                }
            }
        }

        public async Task<List<ProductInfo>> CurrentYearTopFiveProductsByMonth()
        {
            List<ProductInfo> topFiveProductLst = new();
            var lst = await _storage
                .GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            if (lst is null) return new List<ProductInfo>();
            int currentYear = DateTime.Now.Year;
            var currentYearData = lst.Where(d => d.detail_date.Year == currentYear).ToList();

            var topUniqueProductsOfYear = currentYearData
                .GroupBy(s => s.product_name)
                .Select(group => new
                {
                    ProductName = group.Key,
                    TotalQty = group.Sum(s => s.product_qty)
                })
                .OrderByDescending(s => s.TotalQty)
                .Take(5)
                .ToList();

            for (int i = 0; i < topUniqueProductsOfYear.Count; i++)
            {
                var productInfo = new ProductInfo
                {
                    name = topUniqueProductsOfYear[i].ProductName,
                    data = new int[12]
                };

                for (int j = 0; j < 12; j++)
                {
                    var result = currentYearData
                        .Where(s => s.product_name == topUniqueProductsOfYear[i].ProductName)
                        .Where(s => s.detail_date.Month == j + 1)
                        .Sum(s => s.product_qty);

                    productInfo.data[j] = result;
                }

                topFiveProductLst.Add(productInfo);
            }

            return topFiveProductLst;
        }

        public async Task<QtyOfTopFiveProductsByYearModel> QtyOfTopFiveProductsByYear()
        {
            var model = new QtyOfTopFiveProductsByYearModel
            {
                productNames = Array.Empty<string>(),
                productInfos = new List<ProductInfo>()
            };

            var startYear = DateTime.Now.Year;
            var endYear = startYear - 3;

            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            if (lst is null) return model;

            /*var topUniqueProducts = GetTopUniqueProductNames(lst, 4);
            model.productNames = topUniqueProducts.Select(s=> s.);*/

            var topUniqueProducts = lst
                .GroupBy(s => s.product_name)
                .Select(group => new
                {
                    ProductName = group.Key,
                    TotalQty = group.Sum(s => s.product_qty)
                })
                .OrderByDescending(s => s.TotalQty)
                .Take(4)
                .ToList();

            var topN = topUniqueProducts.Count; // 0..4
            if (topN == 0) return model;

            model.productNames = topUniqueProducts.Select(s => s.ProductName).ToArray();
            var topFiveProductLst = new List<ProductInfo>();

            for (int i = endYear; i <= startYear; i++)
            {
                var productInfo = new ProductInfo
                {
                    name = i + " Year",
                    data = new int[topN]
                };

                for (int j = 0; j < topN; j++)
                {
                    var result = lst
                        .Where(s => s.product_name == topUniqueProducts[j].ProductName)
                        .Where(s => s.detail_date.Year == i)
                        .Sum(s => s.product_qty);
                    productInfo.data[j] = result;
                }

                topFiveProductLst.Add(productInfo);
            }

            model.productInfos = topFiveProductLst;
            return model;
        }

        public async Task<PastSevenDaysModel?> PastSevenDays()
        {
            var model = new PastSevenDaysModel();

            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            if (lst is null) return model;

            var currentDate = DateTime.Now;
            var startDate = currentDate.AddDays(-6);
            List<string> daysOfWeekList = new List<string>();
            var endDate = currentDate;
            for (DateTime date = startDate; date <= currentDate; date = date.AddDays(1))
            {
                string dayOfWeek = date.ToString("dddd");
                daysOfWeekList.Add(dayOfWeek);
            }

            model.days = daysOfWeekList.ToArray();
            var sevenDaysData = lst
                .Where(s => s.detail_date >= startDate && s.detail_date <= endDate)
                .ToList();

            var topSixProducts = lst
                .GroupBy(s => s.product_name)
                .Select(group => new
                {
                    ProductName = group.Key,
                    TotalQty = group.Sum(s => s.product_qty)
                })
                .OrderByDescending(s => s.TotalQty)
                .Take(6)
                .ToList();
            var productLst = new List<ProductInfo>();
            for (int i = 0; i < topSixProducts.Count; i++)
            {
                var productInfo = new ProductInfo
                {
                    name = topSixProducts[i].ProductName,
                    data = new int[daysOfWeekList.Count]
                };
                /*foreach (var result in daysOfWeekList.Select(t => sevenDaysData
                             .Where(s => s.product_name == topSixProducts[i].ProductName)
                             .Where(s => s.detail_date.Day.ToString("dddd") == t)
                             .Sum(s => s.product_price)))
                {
                    productInfo.data[i] = result;
                }*/
                foreach (var t in daysOfWeekList)
                {
                    var result = sevenDaysData
                        .Where(s => s.product_name == topSixProducts[i].ProductName)
                        .Where(s => s.detail_date.Day.ToString("dddd") == t)
                        .Sum(s => s.product_price);
                    productInfo.data[i] = result;
                }

                productLst.Add(productInfo);
            }

            model.productInfos = productLst;
            return model;
        }

        public async Task<List<SixMostSoldProductsModel>> SixMostSoldProducts()
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new List<SaleVoucherDetailDataModel>();
            List<SixMostSoldProductsModel> models = new();
            var topSixProducts = lst
                .GroupBy(s => s.product_name)
                .Select(group => new
                {
                    name = group.Key,
                    data = group.Sum(s => s.product_qty)
                })
                .OrderByDescending(s => s.data)
                .Take(6)
                .ToList();
            foreach (var item in topSixProducts)
            {
                models.Add(new SixMostSoldProductsModel
                {
                    name = item.name,
                    data = new List<int>
                    {
                        item.data
                    }
                });
            }

            return models;
        }

        public async Task<List<MaxMinQtyOfProductsModel>> MaxMinQtyOfProducts()
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new List<SaleVoucherDetailDataModel>();
            List<MaxMinQtyOfProductsModel> models = new();
            models = lst
                .GroupBy(product => product.product_name)
                .Select(group => new MaxMinQtyOfProductsModel
                {
                    name = group.Key,
                    low = group.Min(product => product.product_qty),
                    high = group.Max(product => product.product_qty)
                })
                .OrderByDescending(group => group.high)
                .Take(14)
                .ToList();

            return models;
        }

        public async Task<FiveYearLineChart> FiveYearLineChart()
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            int currentYear = DateTime.Now.Year;
            int pastFiveYear = currentYear - 4;
            lst ??= new List<SaleVoucherDetailDataModel>();

            FiveYearLineChart model = new FiveYearLineChart
            {
                pastFiveYear = pastFiveYear,
                productInfos = new List<ProductInfo>()
            };

            var fiveProducts = lst
                .GroupBy(p => p.product_name)
                .Select(s => new
                {
                    ProductName = s.Key,
                    TotalSalePrice = s.Sum(t => t.product_price)
                })
                .OrderByDescending(t => t.TotalSalePrice)
                .Take(5)
                .ToList();

            var fiveYearsData = lst
                .Where(f => f.detail_date.Year <= currentYear
                            && f.detail_date.Year >= pastFiveYear)
                .ToList();

            for (int i = 0; i < fiveProducts.Count; i++)
            {
                var productInfo = new ProductInfo
                {
                    name = fiveProducts[i].ProductName,
                    data = new int[5]
                };

                for (int j = pastFiveYear; j <= currentYear; j++)
                {
                    var result = fiveYearsData
                        .Where(s => s.product_name == fiveProducts[i].ProductName)
                        .Where(s => s.detail_date.Year == j)
                        .Sum(s => s.product_price);
                    // productInfo.data[i] = result;
                    // Console.WriteLine($"j => {j} and j-pastFiveYear = {j-pastFiveYear}");
                    productInfo.data[j - pastFiveYear] = result;
                }

                model.productInfos.Add(productInfo);
            }

            return model;
        }

        public async Task<List<PastFiveYearsMonthlyModel>> PastFiveYearMonthlySaleAmount(DateTime dateTime)
        {
            var year = dateTime.Year;
            var pastFiveYear = year - 5;
            var startDate = dateTime;
            var endDate = dateTime.AddYears(-5);
            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new List<SaleVoucherDetailDataModel>();

            List<int> yearLst = new();
            List<PastFiveYearsMonthlyModel> returnModel = new();
            List<MonthlyModel> monthlyModel = new();
            while (year >= pastFiveYear)
            {
                yearLst.Add(year);
                year = year - 1;
            }

            var dataList = lst
                .Where(x => x.detail_date <= startDate
                            && x.detail_date >= endDate).ToList();
            PastFiveYearsMonthlyModel model = new();
            int count = 0;
            foreach (var item in yearLst)
            {
                foreach (var data in dataList)
                {
                    if (item == data.detail_date.Year && count < 12)
                    {
                        monthlyModel.Add(new MonthlyModel
                        {
                            name = data.detail_date.ToString(),
                            value = data.product_price
                        });
                        count++;
                    }
                }

                count = 0;
                returnModel.Add(new PastFiveYearsMonthlyModel
                {
                    name = item.ToString(),
                    data = monthlyModel
                });
                monthlyModel = new();
            }

            return returnModel;
        }

        public async Task<List<MonthlyRevenueReportResponseModel>> MonthlyRevenueReportForThreeYear()
        {
            List<MonthlyRevenueReportResponseModel> model = new();
            var lst = await _storage
                .GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            if (lst is null) return new List<MonthlyRevenueReportResponseModel>();
            var toYear = DateTime.Now.Year;
            // var fromYear = toYear - 2;

            for (int i = toYear - 2; i <= toYear; i++)
            {
                Console.WriteLine($"The Year = {i}");
                var yearData = lst
                    .Where(l => l.detail_date.Year == i)
                    .ToList();

                var dataLst = new MonthlyRevenueReportResponseModel
                {
                    name = i + " Monthly Revenue Report",
                    data = new List<MonthlyRevenueReportForThreeYear>()
                };

                for (int j = 0; j < 12; j++)
                {
                    var value = yearData
                        .Where(v => v.detail_date.Month == j + 1)
                        .Sum(v => v.product_price);

                    var item = new MonthlyRevenueReportForThreeYear
                    {
                        monthName = (j + 1).GetMonthName(),
                        value = value
                        // value = value.FirstOrDefault()?.value ?? 0
                    };
                    dataLst.data.Add(item);
                }

                model.Add(dataLst);
            }

            /*var threeYearLst = lst
                .Where(l => l.detail_date.Year >= fromYear && l.detail_date.Year <= toYear)
                .ToList();*/

            /*var value = yearData
                        .Where(v => v.detail_date.Month == j + 1)
                        .GroupBy(v => new { v.detail_date.Month })
                        .Select(group => new
                        {
                            value = group.Sum(v => v.product_price)
                        });*/

            return model;
        }

        public async Task<List<ProductCategoryChartModel>> ProductCategoryAndProduct()
        {
            var _productLst = await _storage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            _productLst ??= new List<ProductDataModel>();
            var _productCategoryLst = await _storage
                .GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
            _productCategoryLst ??= new List<ProductCategoryDataModel>();
            List<ProductCategoryChartModel> model = new();
            var sixProductCategory = _productCategoryLst.Take(6).ToList();


            /*for (int i = 0; i < sixProductCategory.Count; i++)
            {
                var pc = new ProductCategoryChartModel
                {
                    name = sixProductCategory[i].product_category_name,
                    data = new List<ProductChartModel>()
                };
                var t = sixProductCategory[i].product_category_code;
                var proLst = _productLst
                    .Where(p => p.product_category_code == sixProductCategory[i].product_category_code)
                    .Select(p => new ProductChartModel
                    {
                        name = p.product_name,
                        value = p.product_sale_price
                    })
                    .Take(5)
                    .ToList();

                pc.data = proLst;
                model.Add(pc);
            }*/

            foreach (var category in sixProductCategory)
            {
                var productsForCategory = _productLst
                    .Where(p => p.product_category_code == category.product_category_code)
                    .Select(p => new ProductChartModel
                    {
                        name = p.product_name,
                        value = p.product_sale_price
                    })
                    .Take(10);

                var pc = new ProductCategoryChartModel
                {
                    name = category.product_category_name,
                    data = productsForCategory.ToList()
                };

                model.Add(pc);
            }

            return model;
        }

        public async Task GenerateDataByMonth()
        {
            var lst = await _storage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            var headLst = await _storage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new List<SaleVoucherDetailDataModel>();
            if (lst.Count == 0)
            {
                List<ProductNameListDataModel>? _lstProduct = await GetProductNameList();
                _lstProduct ??= new List<ProductNameListDataModel>();
                ProductSaleDataModel _model = new();
                Random random = new();

                var startDate = new DateTime(2023, 12, 1);
                var endDate = new DateTime(2023, 1, 1);
                while (startDate >= endDate)
                {
                    // Console.WriteLine($"Start Date {startDate} -- End Date {endDate}");
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            int quantity = random.Next(1, 11);
                            int randomIndex = random.Next(0, _lstProduct.Count);
                            ProductNameListDataModel selectedProduct = _lstProduct[randomIndex];
                            var item = await GetProductName(selectedProduct.product_id);
                            if (item is not null)
                                _model.product_price = item.product_sale_price;

                            _model.product_id = selectedProduct.product_id;
                            _model.product_name = selectedProduct.product_name;
                            _model.product_qty = quantity;
                            _model.product_total_price = quantity * item.product_sale_price;
                            _model.product_sale_date = startDate;

                            if (await CheckIsProductExit(_model.product_sale_id))
                            {
                                await UpdateProductSale(_model);
                            }
                            else
                            {
                                await SetSaleProduct(_model);
                            }
                            // Console.WriteLine($"model => {_model.product_name}");
                        }

                        await SetVoucher();
                        // Console.WriteLine($"{i}  Voucher set!!!!!");
                    }

                    startDate = startDate.AddMonths(-1);
                    // Console.WriteLine("--------------------");
                }
            }

            if (headLst is null)
            {
                DateTime _startDate = DateTime.Now;
                DateTime _endDate = DateTime.Now.AddYears(-5);
                ProductSaleDataModel _model = new();
                var _lstProduct = await GetProductNameList();
                _lstProduct ??= new List<ProductNameListDataModel>();
                while (_startDate >= _endDate)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Random random = new Random();
                            int quantity = random.Next(1, 11);
                            int randomIndex = random.Next(0, _lstProduct.Count);
                            ProductNameListDataModel selectedProduct = _lstProduct[randomIndex];
                            var item = await GetProductName(selectedProduct.product_id);
                            if (item is not null)
                            {
                                _model.product_price = item.product_sale_price;
                                _model.product_id = selectedProduct.product_id;
                                _model.product_name = selectedProduct.product_name;
                                _model.product_qty = quantity;
                                _model.product_total_price = quantity * item.product_sale_price;
                                _model.product_sale_date = _startDate;
                                _model.product_sale_id = Guid.NewGuid();
                            }

                            if (await CheckIsProductExit(_model.product_id))
                            {
                                await UpdateProductSale(_model);
                            }
                            else
                            {
                                await SetSaleProduct(_model);
                            }
                        }

                        await SetVoucher();
                    }

                    _startDate = _startDate.AddYears(-1);
                }
            }
        }

        public async Task GenerateDataAsync(DateTime start, DateTime end, IProgress<double> progress)
        {
            var lstProduct = await GetProductNameList();
            if (lstProduct == null || lstProduct.Count == 0) return;

            var totalDays = (end - start).Days + 1;
            if (totalDays <= 0)
            {
                start = end.AddDays(-30);
                totalDays = 31;
            }

            var random = new Random();
            var currentProcessedDays = 0;

            for (var date = start; date <= end; date = date.AddDays(1))
            {
                // SKIP logic for holidays (randomly skip ~15% of days)
                if (random.Next(1, 101) <= 15)
                {
                    currentProcessedDays++;
                    var p = (double)currentProcessedDays / totalDays * 100;
                    progress.Report(p);
                    continue;
                }

                // Generate 10 to 50 rows (vouchers) per day
                var dailyVouchersCount = random.Next(10, 51);

                for (var i = 0; i < dailyVouchersCount; i++)
                {
                    var itemsInTransaction = random.Next(1, 6);
                    var headId = Guid.NewGuid();
                    var transactionDate = date.AddHours(random.Next(8, 20)).AddMinutes(random.Next(0, 60));

                    var dailyVoucherItems = new List<ProductSaleDataModel>();

                    for (var j = 0; j < itemsInTransaction; j++)
                    {
                        var productSummary = lstProduct[random.Next(lstProduct.Count)];
                        var product = await GetProductName(productSummary.product_id);
                        if (product == null) continue;

                        var qty = random.Next(1, 6);
                        dailyVoucherItems.Add(new ProductSaleDataModel
                        {
                            product_id = product.product_id,
                            product_name = product.product_name,
                            product_price = product.product_sale_price,
                            product_qty = qty,
                            product_total_price = qty * product.product_sale_price,
                            product_sale_date = transactionDate,
                            product_sale_id = Guid.NewGuid()
                        });
                    }

                    if (dailyVoucherItems.Count > 0)
                    {
                        var totalAmount = dailyVoucherItems.Sum(x => x.product_total_price);

                        foreach (var item in dailyVoucherItems)
                        {
                            await SetSaleVoucherDetail(new SaleVoucherDetailDataModel
                            {
                                sale_voucher_detail_id = Guid.NewGuid(),
                                product_id = item.product_id.ToString(),
                                product_name = item.product_name,
                                product_price = item.product_price,
                                product_qty = item.product_qty,
                                sale_voucher_head_id = headId,
                                detail_date = transactionDate
                            });
                        }

                        await SetSaleVoucherHead(new SaleVoucherHeadDataModel
                        {
                            sale_voucher_head_id = headId,
                            sale_total_amount = totalAmount,
                            sale_date = transactionDate,
                            sale_voucher_no = Guid.NewGuid()
                        });
                    }
                }

                currentProcessedDays++;
                var percentage = (double)currentProcessedDays / totalDays * 100;
                progress.Report(percentage);
                await Task.Delay(1);
            }
        }

        private string[] GetProductCategory()
        {
            return new[]
            {
                "Fruit", "Vegetable", "Dairy", "Meat", "Beverage",
                "Snack", "Bakery", "Frozen", "Canned", "Condiment",
                "Cereal", "Grains", "Pasta", "Seafood", "Sweets",
                "Sauce", "Spices", "Tea", "Coffee", "Juice",
                "Water", "Milk", "Cheese", "Eggs", "Poultry",
                "Bread", "Cake", "Cookies", "Ice Cream", "Yogurt",
                "Chips", "Popcorn", "Nuts", "Chocolate", "Candy",
                "Jam", "Mayonnaise", "Pickles", "Oil", "Vinegar",
                "Rice", "Noodles", "Soup", "Salad", "Pizza",
                "Wine", "Beer", "Soda", "Energy Drink", "Liquor",
                "Toothpaste", "Shampoo", "Soap", "Detergent", "Toilet Paper",
                "Towel", "Diapers", "Tissues", "Deodorant", "Lotion",
                "Shaving Cream", "Razor", "Shower Gel", "Sunscreen", "Perfume",
                "Dish Soap", "Hand Soap", "Trash Bags", "Paper Towels", "Candles",
                "Detergent", "Laundry Baskets", "Mop", "Broom", "Sponges",
                "Bucket", "Vacuum", "Iron", "Mop", "Broom",
                "Dustpan", "Waste Bin", "Blender", "Microwave", "Toaster",
                "Kettle", "Coffee Maker", "Food Processor", "Juicer", "Slow Cooker",
                "Rice Cooker", "Waffle Maker", "Grill", "Oven", "Stove",
                "Cutlery", "Dishes", "Glassware", "Cookware", "Bakeware",
                "Utensils", "Containers", "Tupperware", "Plates", "Bowls",
                "Cups", "Saucers", "Mugs", "Pans", "Pots",
                "Spoons", "Forks", "Knives", "Baking Sheets", "Mixing Bowls",
                "Chopping Board", "Can Opener", "Colander", "Strainer", "Grater",
                "Peeler", "Measuring Cups", "Measuring Spoons", "Whisk", "Spatula",
                "Tongs", "Ladle", "Skillet", "Casserole Dish", "Cake Pan",
                "Serving Tray", "Serving Utensils", "Cutting Board", "Salt", "Pepper"
            };
        }

        private string[] GetProduct()
        {
            return new[]
            {
                "Apple",
                "Banana",
                "Orange",
                "Grapes",
                "Strawberry",
                "Mango",
                "Pineapple",
                "Watermelon",
                "Kiwi",
                "Peach",
                "Pear",
                "Cherry",
                "Blueberry",
                "Raspberry",
                "Blackberry",
                "Lemon",
                "Lime",
                "Papaya",
                "Cranberry",
                "Fig",
                "Pomegranate",
                "Avocado",
                "Guava",
                "Plum",
                "Coconut",
                "Passion fruit",
                "Dragon fruit",
                "Lychee",
                "Melon",
                "Apricot",
                "Persimmon",
                "Nectarine",
                "Tangerine",
                "Clementine",
                "Grapefruit",
                "Cantaloupe",
                "Honeydew",
                "Jackfruit",
                "Starfruit",
                "Kiwifruit",
                "Elderberry",
                "Mulberry",
                "Gooseberry",
                "Tamarind",
                "Plantain",
                "Lychee",
                "Ackee",
                "Quince",
                "Date",
                "Olive",
                "Acerola (Barbados cherry)",
                "Breadfruit",
                "Boysenberry",
                "Cactus pear (Prickly pear)",
                "Custard apple",
                "Durian",
                "Feijoa (Pineapple guava)",
                "Jabuticaba",
                "Longan",
                "Mangosteen",
                "Miracle fruit",
                "Noni",
                "Pawpaw",
                "Persimmon",
                "Rambutan",
                "Sapodilla",
                "Soursop",
                "Ugli fruit",
                "White currant",
                "Yangmei (Chinese bayberry)",
                "Horned melon (Kiwano)",
                "Jaboticaba",
                "Loquat",
                "Maracuja (Passionfruit)",
                "Miracle Berry",
                "Monstera Deliciosa (Swiss cheese plant fruit)",
                "Osage orange (Hedge apple)",
                "Pummelo",
                "Salak (Snake fruit)",
                "Sea buckthorn",
                "Surinam cherry",
                "Velvet apple",
                "Wampee",
                "Yuzu",
                "Cranberry",
                "Blackberry",
                "Elderberry",
                "Gooseberry",
                "Mulberry",
                "Raspberry",
                "Blueberry",
                "Boysenberry",
                "Currant",
                "Strawberry",
                "Guava",
                "Kiwi",
                "Kiwi",
                "Lychee",
                "Mango",
                "Papaya",
                "Cranberry",
                "Blackberry",
                "Elderberry",
                "Gooseberry",
                "Mulberry",
                "Raspberry",
                "Blueberry",
                "Boysenberry",
                "Currant",
                "Strawberry",
                "Guava",
                "Kiwi",
                "Kiwi",
                "Lychee",
                "Mango",
                "Papaya",
                "Pineapple",
                "Watermelon",
                "Orange",
                "Grapes",
                "Pear",
                "Cherry",
                "Lemon",
                "Lime",
                "Pomegranate",
                "Plum",
                "Avocado",
                "Dragon fruit",
                "Melon",
                "Fig",
                "Peach",
                "Apricot",
                "Banana",
                "Apple",
                "Passion fruit",
                "Coconut",
                "Tangerine",
                "Clementine",
                "Grapefruit",
                "Cantaloupe",
                "Honeydew",
                "Jackfruit",
                "Starfruit",
                "Kiwifruit",
                "Tamarind",
                "Plantain",
                "Ackee",
                "Quince",
                "Date",
                "Olive",
                "Breadfruit",
                "Cactus pear",
                "Durian",
                "Feijoa",
                "Jabuticaba",
                "Longan",
                "Mangosteen",
                "Miracle fruit",
                "Noni",
                "Pawpaw",
                "Rambutan",
                "Sapodilla",
                "Soursop",
                "Ugli fruit",
                "White currant",
                "Yangmei",
                "Horned melon",
                "Loquat",
                "Maracuja",
                "Miracle Berry",
                "Monstera Deliciosa",
                "Osage orange",
                "Pummelo",
                "Salak",
                "Sea buckthorn",
                "Surinam cherry",
                "Velvet apple",
                "Wampee",
                "Yuzu"
            };
        }
    }
}