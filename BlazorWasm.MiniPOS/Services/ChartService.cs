using System.Globalization;
using Blazored.LocalStorage;
using BlazorWasm.MiniPOS.Models;
using BlazorWasm.MiniPOS.Pages.Reports.Charts;
using TopFiveProducts = BlazorWasm.MiniPOS.Pages.Reports.Charts.MonthlyTopFiveProductsOfCurrentYear;

namespace BlazorWasm.MiniPOS.Services;

public class ChartService
{
    private readonly ILocalStorageService _localStorage;

    public ChartService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    
}