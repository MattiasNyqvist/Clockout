using Microsoft.JSInterop;

namespace WorkTimeCalculator.Wasm.Services;

public enum TimeSystem
{
    /// <summary>Halv åtta = 7,5</summary>
    Decimal,
    /// <summary>Halv åtta = 7:30</summary>
    Clock
}

public enum WeekStart
{
    Monday,
    Sunday
}

public class AppSettings
{
    private const string StorageKey = "worktime.settings";
    private readonly IJSRuntime _js;
    private bool _loaded;

    public AppSettings(IJSRuntime js) => _js = js;

    public TimeSystem TimeSystem { get; set; } = TimeSystem.Decimal;
    public WeekStart WeekStart { get; set; } = WeekStart.Monday;
    public int NormalLunchMinutes { get; set; } = 45;
    public int NormalWorkMinutes { get; set; } = 462; // 7,7 h

    public async Task LoadAsync()
    {
        if (_loaded) return;
        _loaded = true;
        try
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (string.IsNullOrEmpty(json)) return;
            var data = System.Text.Json.JsonSerializer.Deserialize<Data>(json);
            if (data is null) return;
            TimeSystem = data.TimeSystem;
            NormalLunchMinutes = data.NormalLunchMinutes;
            WeekStart = data.WeekStart;
            if (data.NormalWorkMinutes > 0) NormalWorkMinutes = data.NormalWorkMinutes;
        }
        catch { /* ignorera trasig lagring */ }
    }

    public async Task SaveAsync()
    {
        try
        {
            var json = System.Text.Json.JsonSerializer.Serialize(new Data(TimeSystem, NormalLunchMinutes, NormalWorkMinutes, WeekStart));
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }
        catch { }
    }

    private record Data(TimeSystem TimeSystem, int NormalLunchMinutes, int NormalWorkMinutes = 0, WeekStart WeekStart = WeekStart.Monday);
}
