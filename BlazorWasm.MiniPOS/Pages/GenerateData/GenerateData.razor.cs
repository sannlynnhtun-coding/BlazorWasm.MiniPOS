using BlazorWasm.MiniPOS.Models;

namespace BlazorWasm.MiniPOS.Pages.GenerateData;

public partial class GenerateData
{
    private DateTime _startDate = DateTime.Now.AddMonths(-1);
    private DateTime _endDate = DateTime.Now;
    private bool _isGenerating = false;
    private double _progress = 0;

    private async Task GenerateDataByDate()
    {
        if (_isGenerating) return;

        try
        {
            _isGenerating = true;
            _progress = 0;
            StateHasChanged();

            var progressReporter = new Progress<double>(p =>
            {
                _progress = p;
                StateHasChanged();
            });

            await db.GenerateDataAsync(_startDate, _endDate, progressReporter);
        }
        finally
        {
            _isGenerating = false;
            _progress = 100;
            StateHasChanged();
        }
    }
}