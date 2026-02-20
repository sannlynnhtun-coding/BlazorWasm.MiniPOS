using Blazored.LocalStorage;
using BlazorWasm.MiniPOS.Models;

namespace BlazorWasm.MiniPOS.Services
{
    public interface IStorageService
    {
        StorageType CurrentType { get; }
        IStorageProvider CurrentProvider { get; }
        event Action OnStorageTypeChanged;
        void SetStorageType(StorageType type);
    }

    public class StorageService : IStorageService
    {
        private const string InitializedKey = "App_StorageType";
        private readonly IServiceProvider _serviceProvider;
        private readonly ISyncLocalStorageService _localStorage;
        private StorageType _currentType;

        public StorageService(IServiceProvider serviceProvider, ISyncLocalStorageService localStorage)
        {
            _serviceProvider = serviceProvider;
            _localStorage = localStorage;
            
            // Load persistent storage type or default to LocalStorage
            var savedType = _localStorage.GetItem<string>(InitializedKey);
            if (Enum.TryParse<StorageType>(savedType, out var result))
            {
                _currentType = result;
            }
            else
            {
                _currentType = StorageType.IndexedDb;
            }
        }

        public StorageType CurrentType => _currentType;

        public IStorageProvider CurrentProvider => _currentType switch
        {
            StorageType.LocalStorage => _serviceProvider.GetRequiredService<LocalStorageProvider>(),
            StorageType.SessionStorage => _serviceProvider.GetRequiredService<SessionStorageProvider>(),
            StorageType.IndexedDb => _serviceProvider.GetRequiredService<IndexedDbProvider>(),
            _ => _serviceProvider.GetRequiredService<LocalStorageProvider>()
        };

        public event Action? OnStorageTypeChanged;

        public void SetStorageType(StorageType type)
        {
            if (_currentType != type)
            {
                _currentType = type;
                _localStorage.SetItem(InitializedKey, type.ToString());
                OnStorageTypeChanged?.Invoke();
            }
        }
    }
}
