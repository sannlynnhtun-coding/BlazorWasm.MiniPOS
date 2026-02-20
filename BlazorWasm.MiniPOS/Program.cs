using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BlazorWasm.MiniPOS;
using BlazorWasm.MiniPOS.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<LocalStorageProvider>();
builder.Services.AddScoped<SessionStorageProvider>();
builder.Services.AddScoped<IndexedDbProvider>();

builder.Services.AddScoped<IStorageService, StorageService>();

// IDbService now uses IStorageService to get the current provider
builder.Services.AddScoped<IDbService, AppDbService>();

builder.Services.AddSingleton<ChartStateContainer>();
await builder.Build().RunAsync();
