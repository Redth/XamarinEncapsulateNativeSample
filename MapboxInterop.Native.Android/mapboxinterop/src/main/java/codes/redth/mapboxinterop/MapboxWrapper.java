package codes.redth.mapboxinterop;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;

import androidx.annotation.NonNull;

import com.mapbox.mapboxsdk.Mapbox;
import com.mapbox.mapboxsdk.annotations.Marker;
import com.mapbox.mapboxsdk.annotations.MarkerOptions;
import com.mapbox.mapboxsdk.camera.CameraPosition;
import com.mapbox.mapboxsdk.geometry.LatLng;
import com.mapbox.mapboxsdk.maps.MapView;
import com.mapbox.mapboxsdk.maps.MapboxMap;
import com.mapbox.mapboxsdk.maps.MapboxMapOptions;
import com.mapbox.mapboxsdk.maps.OnMapReadyCallback;
import com.mapbox.mapboxsdk.maps.Style;

public class MapboxWrapper {
    private final Activity parentActivity;
    private final MapboxWrapperCallback callbackListener;

    private MapView mapView;
    private MapboxMap map;

    public MapboxWrapper(Activity activity, MapboxWrapperCallback listener)
    {
        parentActivity = activity;
        callbackListener = listener;
    }

    public void init(String mapboxAccessToken)
    {
        Mapbox.getInstance(parentActivity, mapboxAccessToken);
    }

    public void onCreate(Bundle savedInstanceState, Double lat, Double lng, Integer zoom)
    {
        MapboxMapOptions options = MapboxMapOptions.createFromAttributes(parentActivity, null)
            .camera(new CameraPosition.Builder()
            .target(new LatLng(lat, lng))
            .zoom(zoom)
            .build());

        mapView = new MapView(parentActivity, options);
        mapView.onCreate(savedInstanceState);
        mapView.getMapAsync(new OnMapReadyCallback() {
            @Override
            public void onMapReady(@NonNull MapboxMap mapboxMap) {

                map = mapboxMap;
                mapboxMap.setStyle(Style.MAPBOX_STREETS, new Style.OnStyleLoaded() {
                    @Override
                    public void onStyleLoaded(@NonNull Style style) {
                        callbackListener.mapReady();
                    }
                });
                mapboxMap.setOnMarkerClickListener(new MapboxMap.OnMarkerClickListener() {
                    @Override
                    public boolean onMarkerClick(@NonNull Marker marker) {
                        callbackListener.markerClicked(marker.getTitle());
                        return true;
                    }
                });
            }
        });
    }

    public View getView()
    {
        return mapView;
    }

    public void addMarker(double lat, double lng, String title, String snippet)
    {
        map.addMarker(new MarkerOptions()
        .position(new LatLng(lat, lng))
        .title(title)
        .setSnippet(snippet));
    }

    public void onStart()
    {
        mapView.onStart();
    }

    public void onResume()
    {
        mapView.onResume();
    }

    public void onPause()
    {
        mapView.onPause();
    }

    public void onStop()
    {
        mapView.onStop();
    }

    public void onLowMemory()
    {
        mapView.onLowMemory();
    }

    public void onDestory()
    {
        mapView.onDestroy();
    }
}

