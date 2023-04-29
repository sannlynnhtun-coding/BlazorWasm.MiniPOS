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

        public async Task<ProductCreationDataModel> GetProduct(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<ProductCreationDataModel>>("Tbl_Product");
            lst ??= new();
            return lst.FirstOrDefault(x => x.product_id == guid);
        }

        public async Task ProductUpdate(ProductCreationDataModel model)
        {
            //var lst = await GetProduct(model.product_id);
            var lst = await GetProductList();
            lst ??= new();
            var result = lst.FirstOrDefault(x => x.product_id == model.product_id);
            int index = lst.FindIndex(x=> x.product_id == result.product_id);
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
            await localStorage.SetItemAsync("Tbl_Product",lst);
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
            return lstProductName;
        }

        public async Task<int> GetProductName(Guid guid)
        {
            var lst = await localStorage.GetItemAsync<List<ProductCreationDataModel>>("Tbl_Product");
            lst ??= new();
            var result = lst.FirstOrDefault(x => x.product_id == guid);
            return result.product_sale_price ;
        }
    }
}
