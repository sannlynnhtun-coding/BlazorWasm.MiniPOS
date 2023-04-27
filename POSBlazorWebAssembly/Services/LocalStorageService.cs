using Blazored.LocalStorage;
using POSBlazorWebAssembly.Models;

namespace POSBlazorWebAssembly.Services
{
    public class LocalStorageService : IDbService
    {
        private readonly ILocalStorageService localStorage;

        public LocalStorageService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<List<ProductCreationDataModel>> GetProductList()
        {
            List<ProductCreationDataModel> lst = await localStorage.GetItemAsync<List<ProductCreationDataModel>>("Tbl_Product");
            lst ??= new();
            return lst.OrderByDescending(x=> x.product_cration_date).ToList();
        }

        public async Task SetProduct(ProductCreationDataModel model)
        {
            List<ProductCreationDataModel> lst = await localStorage.GetItemAsync<List<ProductCreationDataModel>>("Tbl_Product");
            lst ??= new();
            lst.Add(model);
            await localStorage.SetItemAsync("Tbl_Product", lst);
        }
    }
}
