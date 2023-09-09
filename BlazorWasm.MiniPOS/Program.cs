using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BlazorWasm.MiniPOS;
using BlazorWasm.MiniPOS.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddSingleton<IDbService, LocalStorageService>();
builder.Services.AddSingleton<ChartStateContainer>();
await builder.Build().RunAsync();
