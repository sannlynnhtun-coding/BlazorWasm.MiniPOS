namespace BlazorWasm.MiniPOS.Services
{
    public interface IStorageProvider
    {
        Task<T?> GetItemAsync<T>(string key);
        Task SetItemAsync<T>(string key, T data);
        Task RemoveItemAsync(string key);
        Task ClearAsync();
    }
}
