using Blazored.SessionStorage;

namespace BlazorWasm.MiniPOS.Services
{
    public class SessionStorageProvider : IStorageProvider
    {
        private readonly ISessionStorageService _sessionStorage;

        public SessionStorageProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task<T?> GetItemAsync<T>(string key) => 
            await _sessionStorage.GetItemAsync<T>(key);

        public async Task SetItemAsync<T>(string key, T data) => 
            await _sessionStorage.SetItemAsync(key, data);

        public async Task RemoveItemAsync(string key) => 
            await _sessionStorage.RemoveItemAsync(key);

        public async Task ClearAsync() => 
            await _sessionStorage.ClearAsync();
    }
}
