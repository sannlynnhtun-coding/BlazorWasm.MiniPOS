using Blazored.LocalStorage;

namespace BlazorWasm.MiniPOS.Services
{
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly ILocalStorageService _localStorage;

        public LocalStorageProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<T?> GetItemAsync<T>(string key) => 
            await _localStorage.GetItemAsync<T>(key);

        public async Task SetItemAsync<T>(string key, T data) => 
            await _localStorage.SetItemAsync(key, data);

        public async Task RemoveItemAsync(string key) => 
            await _localStorage.RemoveItemAsync(key);

        public async Task ClearAsync() => 
            await _localStorage.ClearAsync();
    }
}
