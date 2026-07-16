using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EcoMeal.client.Components.Map;

/// <summary>
/// Read-only map that displays a single static marker at a given lat/lng.
/// Wraps mapViewer.js via JS Interop.
/// </summary>
public partial class MapViewer : IAsyncDisposable
{
    // ── Parameters ──────────────────────────────────────────────────────────

    /// <summary>Mapbox public access token.</summary>
    [Parameter, EditorRequired]
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>Latitude to centre the map and place the marker.</summary>
    [Parameter, EditorRequired]
    public double Latitude { get; set; }

    /// <summary>Longitude to centre the map and place the marker.</summary>
    [Parameter, EditorRequired]
    public double Longitude { get; set; }

    /// <summary>CSS height of the map div. Defaults to 300px.</summary>
    [Parameter] public string Height { get; set; } = "300px";

    /// <summary>Whether to show the lat/lng text below the map.</summary>
    [Parameter] public bool ShowCoordinates { get; set; } = false;

    // ── Injected services ────────────────────────────────────────────────────
    [Inject] private IJSRuntime JS { get; set; } = default!;

    // ── Internal state ───────────────────────────────────────────────────────
    private readonly string _containerId = $"map-viewer-{Guid.NewGuid():N}";
    private bool _mapInitialised;

    // ── Lifecycle ────────────────────────────────────────────────────────────

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !_mapInitialised)
        {
            _mapInitialised = true;

            await JS.InvokeVoidAsync(
                "mapViewerInit",
                _containerId,
                AccessToken,
                Latitude,
                Longitude);
        }
    }

    // ── Disposal ─────────────────────────────────────────────────────────────

    public async ValueTask DisposeAsync()
    {
        try
        {
            await JS.InvokeVoidAsync("mapViewerDestroy", _containerId);
        }
        catch { /* Ignore JS errors during teardown */ }
    }
}
