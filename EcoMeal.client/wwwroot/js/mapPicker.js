// mapPicker.js — Editable Mapbox map for admin location selection
// Requires Mapbox GL JS loaded before this file.

const _pickerMaps = {};   // containerId -> { map, marker }

/**
 * Initialise an editable map inside the given container div.
 * @param {string}   containerId   - ID of the <div> to render into
 * @param {string}   accessToken   - Mapbox public access token
 * @param {number}   initialLat    - Starting latitude  (nullable → use fallback)
 * @param {number}   initialLng    - Starting longitude (nullable → use fallback)
 * @param {object}   dotNetRef     - DotNetObjectReference for Blazor callbacks
 */
window.mapPickerInit = function (containerId, accessToken, initialLat, initialLng, dotNetRef) {
    // Destroy any previous instance for this container
    if (_pickerMaps[containerId]) {
        _pickerMaps[containerId].map.remove();
        delete _pickerMaps[containerId];
    }

    mapboxgl.accessToken = accessToken;

    // Fallback: Bucharest city centre
    const fallbackLng = 26.1025;
    const fallbackLat = 44.4268;
    const startLng = (initialLng != null && !isNaN(initialLng)) ? initialLng : fallbackLng;
    const startLat = (initialLat != null && !isNaN(initialLat)) ? initialLat : fallbackLat;
    const hasInitial = (initialLat != null && !isNaN(initialLat));

    const map = new mapboxgl.Map({
        container: containerId,
        style: 'mapbox://styles/mapbox/streets-v12',
        center: [startLng, startLat],
        zoom: hasInitial ? 15 : 12
    });

    map.addControl(new mapboxgl.NavigationControl(), 'top-right');
    map.addControl(new mapboxgl.FullscreenControl(), 'top-right');

    // Draggable marker
    const marker = new mapboxgl.Marker({ color: '#2ecc71', draggable: true })
        .setLngLat([startLng, startLat])
        .addTo(map);

    // Notify Blazor of the initial position
    if (hasInitial) {
        _notifyBlazor(dotNetRef, startLat, startLng);
    }

    // Click on map → move marker
    map.on('click', (e) => {
        const { lng, lat } = e.lngLat;
        marker.setLngLat([lng, lat]);
        _notifyBlazor(dotNetRef, lat, lng);
    });

    // Drag end → notify
    marker.on('dragend', () => {
        const { lng, lat } = marker.getLngLat();
        _notifyBlazor(dotNetRef, lat, lng);
    });

    _pickerMaps[containerId] = { map, marker };
};

/**
 * Resize the map (call after the container becomes visible / size changes).
 */
window.mapPickerResize = function (containerId) {
    const inst = _pickerMaps[containerId];
    if (inst) inst.map.resize();
};

/**
 * Destroy the map instance and clean up.
 */
window.mapPickerDestroy = function (containerId) {
    const inst = _pickerMaps[containerId];
    if (inst) {
        inst.map.remove();
        delete _pickerMaps[containerId];
    }
};

function _notifyBlazor(dotNetRef, lat, lng) {
    if (dotNetRef) {
        dotNetRef.invokeMethodAsync('OnMarkerMoved', lat, lng).catch(console.error);
    }
}
