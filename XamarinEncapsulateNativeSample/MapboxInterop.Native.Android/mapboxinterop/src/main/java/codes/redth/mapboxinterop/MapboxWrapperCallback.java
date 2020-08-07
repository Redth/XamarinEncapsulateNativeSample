package codes.redth.mapboxinterop;

import com.mapbox.mapboxsdk.maps.MapboxMap;

public interface MapboxWrapperCallback {

    void mapReady();

    void markerClicked(String title);
}
