using POSBlazorWebAssembly.Models;

namespace POSBlazorWebAssembly.Services
{
    public interface IDbService
    {
        Task<bool> CheckIsProductExit(Guid guid);
        Task DeleteProduct(Guid guid);
        Task DeleteProductCategory(Guid guid);
        Task DeleteProductSale(Guid guid);
        Task<ProductSaleDataModel> EditProductSale(Guid guid);
        Task<int> GetGrandTotal();
        Task<ProductDataModel> GetProduct(Guid guid);
        Task<ProductCategoryDataModel> GetProductCategory(Guid guid);
        Task<List<ProductCategoryDataModel>> GetProductCategoryList();
        Task<List<ProductDataModel>> GetProductList();
        Task<ProductDataModel> GetProductName(Guid guid);
        Task<List<ProductNameListDataModel>> GetProductNameList();
        Task<ProductSaleResponseDataModel> GetRecentProductSale(int pageNo = 1, int pageSize = 5);
        Task ProductCategoryUpdate(ProductCategoryDataModel model);
        Task<ProductSaleResponseDataModel> ProductSalePagination(int pageNo, int pageSize);
        Task ProductUpdate(ProductDataModel model);
        Task<SaleReportResponseDataModel> SaleReport(DateTime dateTime);
        Task<SaleReportResponseDataModel> SaleReportPagination(int pageNo, int pageSize, DateTime dateTime);
        Task SetProduct(ProductDataModel model);
        Task SetProductCategory(ProductCategoryDataModel model);
        Task SetSaleProduct(ProductSaleDataModel model);
        Task UpdateProductSale(ProductSaleDataModel model);
    }
}