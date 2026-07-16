using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EcoMeal.client.Components.Map;

public partial class MapPicker : IAsyncDisposable
{
   
    [Parameter, EditorRequired]
    public string AccessToken { get; set; } = string.Empty;

    [Parameter] public double? InitialLat { get; set; }

    [Parameter] public double? InitialLng { get; set; }

    [Parameter] public EventCallback<(double Lat, double Lng)> OnLocationChanged { get; set; }

    [Parameter] public EventCallback OnLocationCleared { get; set; }

    [Inject] private IJSRuntime JS { get; set; } = default!;

    private readonly string _containerId = $"map-picker-{Guid.NewGuid():N}";
    private DotNetObjectReference<MapPicker>? _selfRef;
    private double? _lat;
    private double? _lng;
    private bool _mapInitialised;
    protected override void OnInitialized()
    {
        _lat = InitialLat;
        _lng = InitialLng;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !_mapInitialised)
        {
            _selfRef = DotNetObjectReference.Create(this);
            _mapInitialised = true;

            await JS.InvokeVoidAsync(
                "mapPickerInit",
                _containerId,
                AccessToken,
                InitialLat.HasValue ? (object)InitialLat.Value : null,
                InitialLng.HasValue ? (object)InitialLng.Value : null,
                _selfRef);
        }
    }

  
    [JSInvokable]
    public async Task OnMarkerMoved(double lat, double lng)
    {
        _lat = lat;
        _lng = lng;
        StateHasChanged();
        await OnLocationChanged.InvokeAsync((_lat.Value, _lng.Value));
    }

       private async Task ClearLocation()
    {
        _lat = null;
        _lng = null;
        StateHasChanged();
        await OnLocationCleared.InvokeAsync();
    }
    public async ValueTask DisposeAsync()
    {
        try
        {
            await JS.InvokeVoidAsync("mapPickerDestroy", _containerId);
        }
        catch { }

        _selfRef?.Dispose();
    }
}
