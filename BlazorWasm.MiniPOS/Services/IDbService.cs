using BlazorWasm.MiniPOS.Models;
using BlazorWasm.MiniPOS.Pages.Reports.Charts;

namespace BlazorWasm.MiniPOS.Services
{
    public interface IDbService
    {
        Task<List<BestProductReportModel>> BestProductReport();
        Task<bool> CheckIsProductExit(Guid guid);
        Task<List<ProductInfo>> CurrentYearTopFiveProductsByMonth();
        Task DeleteProduct(Guid guid);
        Task DeleteProductCategory(Guid guid);
        Task DeleteProductSale(Guid guid);
        Task<DonutChartResponseModel> DonutChart();
        Task<ProductSaleDataModel> EditProductSale(Guid guid);
        Task GenerateDataByMonth();
        Task GenerateYearOverYear();
        Task<int> GetGrandTotal();
        Task<ProductDataModel> GetProduct(Guid guid);
        Task<ProductCategoryDataModel> GetProductCategory(Guid guid);
        Task<List<ProductCategoryDataModel>> GetProductCategoryList();
        Task<List<ProductDataModel>> GetProductList();
        Task<ProductDataModel?> GetProductName(Guid guid);
        Task<List<ProductNameListDataModel>> GetProductNameList();
        Task<ProductSaleResponseDataModel> GetRecentProductSale(int pageNo = 1, int pageSize = 5);
        Task<List<SaleVoucherHeadDataModel>> GetSaleVoucherHead();
        Task<List<SaleVoucherDetailDataModel>> GetVoucherDetail(Guid guid);
        Task<DonutChartResponseModel> PastFiveYear(DateTime date);
        Task<PastFiveYearModel> PastFiveYearV1(DateTime date);
        Task ProductCategoryUpdate(ProductCategoryDataModel model);
        Task<ProductSaleResponseDataModel> ProductSalePagination(int pageNo, int pageSize);
        Task ProductUpdate(ProductDataModel model);
        Task<List<ProductInfo>> QtyOfTopFiveProductsByYear();
        Task<SaleReportResponseDataModel> SaleReport(DateTime dateTime);
        Task<SaleReportResponseDataModel> SaleReportPagination(int pageNo, int pageSize, DateTime dateTime);
        Task SetProduct(ProductDataModel model);
        Task SetProductCategory(ProductCategoryDataModel model);
        Task SetSaleProduct(ProductSaleDataModel model);
        Task SetVoucher();
        Task UpdateProductSale(ProductSaleDataModel model);
        Task<YearOverYearReturnModel> YearOverYearChart(DateTime dateTime);
        Task<DataReturnInfo> PastFiveYearFunnelChart(DateTime date);
        Task<List<int>> GetYearList();
        Task<List<TwoYearComparisonModel>> CompareTwoYear(int firstYear,
            int secondYear);
    }
}