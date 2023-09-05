using System.Globalization;
using System.Text.Json;
using ApexCharts;
using Blazored.LocalStorage;
using BlazorWasm.MiniPOS.Models;
using BlazorWasm.MiniPOS.Pages.Reports.Charts;
using TopFiveProducts = BlazorWasm.MiniPOS.Pages.Reports.Charts.MonthlyTopFiveProductsOfCurrentYear;
using System.Linq;

namespace BlazorWasm.MiniPOS.Services
{
    public class LocalStorageService : IDbService
    {
        private readonly ILocalStorageService _localStorage;

        public LocalStorageService(ILocalStorageService localStorage)
        {
            this._localStorage = localStorage;
        }

        public async Task<List<ProductDataModel>> GetProductList()
        {
            //bool localStorageName = await JsRuntime.InvokeAsync<bool>("Tbl_Product");
            var lst = await _localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
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
                    product_buying_price = r.Next(1000, 100000),
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

            await _localStorage.SetItemAsync("Tbl_Product", lst);

            return lst.Any()
                ? lst.OrderByDescending(x => x.product_creation_date).ToList()
                : new List<ProductDataModel>();
        }

        public async Task SetProduct(ProductDataModel model)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new();
            lst.Add(model);
            await _localStorage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task<ProductDataModel> GetProduct(Guid guid)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
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

            await _localStorage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task DeleteProduct(Guid guid)
        {
            var lst = await GetProductList();
            var item = lst.FirstOrDefault(x => x.product_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await _localStorage.SetItemAsync("Tbl_Product", lst);
        }

        public async Task<List<ProductCategoryDataModel>> GetProductCategoryList()
        {
            var lst = await _localStorage
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

            await _localStorage.SetItemAsync("Tbl_ProductCategory", lst);
            return lst
                .OrderByDescending(x => x.product_category_creation_date)
                .ThenByDescending(x => x.product_category_code)
                .ToList();
        }

        public async Task SetProductCategory(ProductCategoryDataModel model)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
            lst ??= new();
            lst.Add(model);
            await _localStorage.SetItemAsync("Tbl_ProductCategory", lst);
        }

        public async Task<ProductCategoryDataModel> GetProductCategory(Guid guid)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductCategoryDataModel>>("Tbl_ProductCategory");
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

            await _localStorage.SetItemAsync("Tbl_ProductCategory", lst);
        }

        public async Task DeleteProductCategory(Guid guid)
        {
            var lst = await GetProductCategoryList();
            var item = lst.FirstOrDefault(x => x.product_category_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await _localStorage.SetItemAsync("Tbl_ProductCategory", lst);
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
            var lst = await _localStorage.GetItemAsync<List<ProductDataModel>>("Tbl_Product");
            lst ??= new List<ProductDataModel>();
            var result = lst.FirstOrDefault(x => x.product_id == guid);
            return result ?? throw new InvalidOperationException();
        }

        public async Task SetSaleProduct(ProductSaleDataModel model)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            lst.Add(model);
            await _localStorage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<int> GetGrandTotal()
        {
            var lst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            return lst.Select(x => x.product_total_price).Sum();
        }

        public async Task<ProductSaleResponseDataModel> GetRecentProductSale(int pageNo = 1, int pageSize = 5)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
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
            var lst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
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
            var lst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_sale_id == guid);
            if (item == null) return;
            lst.Remove(item);
            await _localStorage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<ProductSaleDataModel> EditProductSale(Guid guid)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_sale_id == guid);
            if (item == null) return new ProductSaleDataModel();
            return item;
        }

        public async Task<bool> CheckIsProductExit(Guid guid)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
            lst ??= new();
            var item = lst.FirstOrDefault(x => x.product_id == guid);
            return item != null;
        }

        public async Task UpdateProductSale(ProductSaleDataModel model)
        {
            var lst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
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

            await _localStorage.SetItemAsync("Tbl_ProductSale", lst);
        }

        public async Task<SaleReportResponseDataModel> SaleReport(DateTime dateTime)
        {
            var lst = await _localStorage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
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
            var lst = await _localStorage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
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
            var lst = await _localStorage
                .GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            var lstProduct = await _localStorage
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
            var lst = await _localStorage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new List<SaleVoucherDetailDataModel>();
            lst.Add(model);
            await _localStorage.SetItemAsync("Tbl_SaleVoucherDetail", lst);
        }

        private async Task SetSaleVoucherHead(SaleVoucherHeadDataModel model)
        {
            var lst = await _localStorage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new List<SaleVoucherHeadDataModel>();
            lst.Add(model);
            await _localStorage.SetItemAsync("Tbl_SaleVoucherHead", lst);
        }

        public async Task SetVoucher()
        {
            //Guid sale_voucher_detail_id = Guid.NewGuid();
            var saleVoucherHeadId = Guid.NewGuid();
            SaleVoucherHeadDataModel headModel = new();
            DateTime voucherDate = DateTime.Now;

            var getLst = await _localStorage.GetItemAsync<List<ProductSaleDataModel>>("Tbl_ProductSale");
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
                await _localStorage.RemoveItemAsync("Tbl_ProductSale");
            }
        }

        public async Task<List<SaleVoucherDetailDataModel>> GetVoucherDetail(Guid guid)
        {
            var lst = await _localStorage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
            lst ??= new List<SaleVoucherDetailDataModel>();
            var result = lst.Where(x => x.sale_voucher_head_id == guid).ToList();
            return result;
        }

        public async Task<List<SaleVoucherHeadDataModel>> GetSaleVoucherHead()
        {
            var lst = await _localStorage.GetItemAsync<List<SaleVoucherHeadDataModel>>("Tbl_SaleVoucherHead");
            lst ??= new List<SaleVoucherHeadDataModel>();
            return lst;
        }

        public async Task<YearOverYearReturnModel> YearOverYearChart(DateTime dateTime)
        {
            var year = dateTime.Year;
            var pastThreeYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <=year && x.sale_date.Year >= pastThreeYear)
                .GroupBy(s=> s.sale_date.Year).Select(s=> new YearOverYearResponseModel
                {
                    Year = s.Key, 
                    TotalPrice = s.Sum(sale => sale.sale_total_amount)
                }).ToList();
            
            YearOverYearReturnModel returnModel = new();
            List<YearOverYearModel> yearData = new();
            for (var i = 1; i < dataList.Count; i++)
            {
                returnModel.Year.Add(dataList[0].Year.ToString() +"/"+ 
                    dataList[i].Year.ToString());

                yearData.Add(new YearOverYearModel
                {
                    Year = dataList[i].Year.ToString(),
                    Data = new List<long> { Convert.ToInt64(dataList[0].TotalPrice), Convert.ToInt64(dataList[i].TotalPrice) } 
                });
            }
            returnModel.YearData = yearData;
            return returnModel;
        }

        public async Task<List<DataInfo>> PastFiveYearV1(DateTime date)
        {
            var year = date.Year;
            var pastThreeYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <= year && x.sale_date.Year >= pastThreeYear)
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
            return data;
        }
        public async Task<object[][]> PastFiveYear(DateTime date)
        {
            var year = date.Year;
            var pastThreeYear = year - 5;
            var lst = await GetSaleVoucherHead();
            var dataList = lst
                .Where(x => x.sale_date.Year <= year && x.sale_date.Year >= pastThreeYear)
                .GroupBy(s => s.sale_date.Year).Select(s => new
                {
                    Year = s.Key,
                    TotalPrice = s.Sum(sale => sale.sale_total_amount)
                }).ToList();
            object[][] data = new object[dataList.Count][];
            foreach(var item in dataList)
            {
                data = new object[][]
                {
                    new object[] { item.Year.ToString(), item.TotalPrice }
                };
            }

            return data;
        }

        public async Task GenerateYearOverYear()
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

        public async Task<List<ProductInfo>> CurrentYearTopFiveProductsByMonth()
        {
            List<ProductInfo> topFiveProductLst = new();
            var lst = await _localStorage
                .GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");

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
        
        public async Task GenerateDataByMonth()
        {
            var lst = await _localStorage.GetItemAsync<List<SaleVoucherDetailDataModel>>("Tbl_SaleVoucherDetail");
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
                    Console.WriteLine($"Start Date {startDate} -- End Date {endDate}");
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
                            Console.WriteLine($"model => {_model.product_name}");
                        }
                        await SetVoucher();
                        Console.WriteLine($"{i}  Voucher set!!!!!");
                    }
                    startDate = startDate.AddMonths(-1);
                    Console.WriteLine("--------------------");
                }
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

                // "Apple", "Banana", "Orange", "Grapes", "Strawberry",
                // "Lettuce", "Tomato", "Carrot", "Broccoli", "Spinach",
                // "Milk", "Cheese", "Yogurt", "Butter", "Cream",
                // "Chicken", "Beef", "Pork", "Fish", "Shrimp",
                // "Chips", "Popcorn", "Nuts", "Chocolate", "Candy",
                // "Bread", "Cake", "Cookies", "Ice Cream", "Brownie",
                // "Coffee", "Tea", "Juice", "Water", "Soda",
                // "Rice", "Pasta", "Burger", "Pizza", "Sandwich",
                // "Soap", "Shampoo", "Toothpaste", "Deodorant", "Lotion",
                // "Shaving Cream", "Razor", "Toilet Paper", "Towel", "Diapers",
                // "Dish Soap", "Laundry Detergent", "Trash Bags", "Paper Towels", "Candles",
                // "Dishwasher Detergent", "Fabric Softener", "Mop", "Broom", "Sponges",
                // "Bucket", "Vacuum Cleaner", "Iron", "Blender", "Microwave",
                // "Toaster", "Kettle", "Food Processor", "Juicer", "Slow Cooker",
                // "Rice Cooker", "Waffle Maker", "Grill", "Oven", "Stove",
                // "Cutlery Set", "Dinnerware", "Cookware", "Bakeware", "Utensils",
                // "Storage Containers", "Plates", "Bowls", "Cups", "Mugs",
                // "Pans", "Pots", "Spoons", "Forks", "Knives",
                // "Measuring Cups", "Mixing Bowls", "Cutting Board", "Colander", "Strainer",
                // "Peeler", "Whisk", "Spatula", "Tongs", "Ladle",
                // "Cake Pan", "Baking Sheet", "Rolling Pin", "Can Opener", "Grater",
                // "Serving Tray", "Cutting Board", "Salt", "Pepper",
            };
        }
    }
}