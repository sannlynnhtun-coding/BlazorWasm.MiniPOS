using Microsoft.JSInterop;

namespace BlazorWasm.MiniPOS.Services
{
    public class IndexedDbProvider : IStorageProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public IndexedDbProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            return await _jsRuntime.InvokeAsync<T?>("indexedDbGet", key);
        }

        public async Task SetItemAsync<T>(string key, T data)
        {
            await _jsRuntime.InvokeVoidAsync("indexedDbSet", key, data);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("indexedDbRemove", key);
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("indexedDbClear");
        }
    }
}
