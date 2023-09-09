using System.Globalization;
using Blazored.LocalStorage;
using BlazorWasm.MiniPOS.Models;
using TopFiveProducts = BlazorWasm.MiniPOS.Pages.Dashboard.Model.MonthlyTopFiveProductsOfCurrentYear;

namespace BlazorWasm.MiniPOS.Services;

public class ChartService
{
    private readonly ILocalStorageService _localStorage;

    public ChartService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    
}