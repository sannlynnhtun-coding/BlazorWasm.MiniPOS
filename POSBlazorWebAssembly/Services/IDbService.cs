using POSBlazorWebAssembly.Models;

namespace POSBlazorWebAssembly.Services
{
    public interface IDbService
    {
        Task<List<ProductCreationDataModel>> GetProductList();
        Task SetProduct(ProductCreationDataModel model);
    }
}
