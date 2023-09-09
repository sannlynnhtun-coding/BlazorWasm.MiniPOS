namespace BlazorWasm.MiniPOS.Services
{
    public class ChartStateContainer
    {
        private string? chartName;
        private bool? firstShow;
        public string ChartName
        {
            get => chartName ?? string.Empty;
            set
            {
                chartName = value;
                NotifyStateChanged();
            }
        }
        public bool DashboardShow
        {
            get => firstShow ?? false;
            set
            {
                firstShow = value;
                NotifyStateChanged();
            }
        }
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
