import Foundation
import Mapbox

class MapViewDelegate : NSObject, MGLMapViewDelegate
{
    var callback:MapboxWrapperCallback
    public var mapStyle:MGLStyle
    
    public init(callback:MapboxWrapperCallback)
    {
        self.mapStyle = MGLStyle()
        self.callback = callback
    }
    
    public func mapView(_ mapView: MGLMapView, didFinishLoading style: MGLStyle) {
        self.mapStyle = style
        self.callback.mapReady()
    }
    
    public func mapView(_ mapView: MGLMapView, didSelect annotation: MGLAnnotation) {
        if (annotation.title != nil)
        {
            self.callback.markerClicked(title: ((annotation.title) ?? "") ?? "")
        }
    }
}

@objc
public class MapboxWrapper: NSObject
{
    var mapView:MGLMapView
    var callback:MapboxWrapperCallback
    var mapViewDelegate:MapViewDelegate
    
    @objc
    public init(frame:CGRect, callback:MapboxWrapperCallback) {
        
        self.callback = callback
        self.mapViewDelegate = MapViewDelegate(callback: self.callback)
        
        let url = URL(string:"mapbox://styles/mapbox/streets-v11")
        
        mapView = MGLMapView(frame: frame, styleURL: url)
        
        super.init()
        
        mapView.autoresizingMask = [.flexibleWidth, .flexibleHeight]
        mapView.delegate = self.mapViewDelegate
        mapView.setCenter(
            CLLocationCoordinate2D(latitude: 59.31, longitude: 18.06),
            animated: false)
    }
    
    @objc
    public func getView() -> UIView
    {
        return mapView
    }
    
    @objc
    public func addMarker(lat:Double, lng:Double, title:String, snippet:String)
    {
        let point = MGLPointAnnotation()
        point.title = title
        point.coordinate = CLLocationCoordinate2D(latitude: lat, longitude: lng)
        point.title = title;
        point.subtitle = snippet;
        
        self.mapView.addAnnotation(point);
//        let src = MGLShapeSource(identifier: title + "-src", shape: point, options: nil)
//        let layer = MGLSymbolStyleLayer(identifier: title + "-style", source: src)
//
//        self.mapViewDelegate.mapStyle.addSource(src)
//        self.mapViewDelegate.mapStyle.addLayer(layer)
    }
}

@objc
public protocol MapboxWrapperCallback {
    func markerClicked(title: String)
    func mapReady()
}
