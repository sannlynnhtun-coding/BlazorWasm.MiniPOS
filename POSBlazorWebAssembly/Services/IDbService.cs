using POSBlazorWebAssembly.Models;

namespace POSBlazorWebAssembly.Services
{
    public interface IDbService
    {
        Task<List<ProductCreationDataModel>> GetProductList();
        Task SetProduct(ProductCreationDataModel model);
        Task<ProductCreationDataModel> GetProduct(Guid guid);
        Task ProductUpdate(ProductCreationDataModel model);
        Task DeleteProduct(Guid guid);
        Task<List<ProductCategoryDataModel>> GetProductCategoryList();
        Task SetProductCategory(ProductCategoryDataModel model);
        Task<ProductCategoryDataModel> GetProductCategory(Guid guid);
        Task ProductCategoryUpdate(ProductCategoryDataModel model);
        Task DeleteProductCategory(Guid guid);
        Task<List<ProductNameListDataModel>> GetProductNameList();
        Task<ProductCreationDataModel> GetProductName(Guid guid);
        Task SetSaleProduct(ProductSaleDataModel model);
        Task<int> GetGrandTotal();
        Task<ProductSaleResponseDataModel> GetRecentProductSale();
        Task<ProductSaleResponseDataModel> ProductSalePagination(int pageNo, int pageSize);
        Task DeleteProductSale(Guid guid);
        Task<ProductSaleDataModel> EditProductSale(Guid guid);
        Task<bool> CheckIsProductExit(Guid guid);
        Task UpdateProductSale(ProductSaleDataModel model);
    }
}
