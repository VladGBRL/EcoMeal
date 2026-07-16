// mapViewer.js — Read-only Mapbox map for displaying a business location
// Requires Mapbox GL JS loaded before this file.

const _viewerMaps = {};   // containerId -> { map, marker }

/**
 * Initialise a read-only map centred on the given coordinates.
 * @param {string} containerId  - ID of the <div> to render into
 * @param {string} accessToken  - Mapbox public access token
 * @param {number} lat          - Latitude of the business
 * @param {number} lng          - Longitude of the business
 */
window.mapViewerInit = function (containerId, accessToken, lat, lng) {
    // Destroy any previous instance for this container
    if (_viewerMaps[containerId]) {
        _viewerMaps[containerId].map.remove();
        delete _viewerMaps[containerId];
    }

    mapboxgl.accessToken = accessToken;

    const map = new mapboxgl.Map({
        container: containerId,
        style: 'mapbox://styles/mapbox/streets-v12',
        center: [lng, lat],
        zoom: 15,
        interactive: false       // disables pan, zoom, click — purely read-only
    });

    // Re-enable scroll-zoom and drag-pan only for touch/desktop feel but keep clicks silent
    // (interactive: false already disables everything, which is what we want for a viewer)

    // Static marker — not draggable
    const marker = new mapboxgl.Marker({ color: '#2ecc71', draggable: false })
        .setLngLat([lng, lat])
        .addTo(map);

    _viewerMaps[containerId] = { map, marker };
};

/**
 * Resize the viewer map (call after container visibility changes).
 */
window.mapViewerResize = function (containerId) {
    const inst = _viewerMaps[containerId];
    if (inst) inst.map.resize();
};

/**
 * Destroy the viewer map instance.
 */
window.mapViewerDestroy = function (containerId) {
    const inst = _viewerMaps[containerId];
    if (inst) {
        inst.map.remove();
        delete _viewerMaps[containerId];
    }
};
