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
    }
}
