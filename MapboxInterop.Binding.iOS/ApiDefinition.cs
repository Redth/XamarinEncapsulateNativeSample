using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MapboxInterop
{
	// @interface MapboxWrapper : NSObject
	[BaseType(typeof(NSObject), Name = "_TtC13MapboxInterop13MapboxWrapper")]
	[DisableDefaultCtor]
	interface MapboxWrapper
	{
		// -(instancetype _Nonnull)initWithFrame:(CGRect)frame callback:(id<MapboxWrapperCallback> _Nonnull)callback __attribute__((objc_designated_initializer));
		[Export("initWithFrame:callback:")]
		[DesignatedInitializer]
		IntPtr Constructor(CGRect frame, IMapboxWrapperCallback callback);

		// -(UIView * _Nonnull)getView __attribute__((warn_unused_result("")));
		[Export("getView")]
		UIView View { get; }

		// -(void)addMarkerWithLat:(double)lat lng:(double)lng title:(NSString * _Nonnull)title snippet:(NSString * _Nonnull)snippet;
		[Export("addMarkerWithLat:lng:title:snippet:")]
		void AddMarker(double lat, double lng, string title, string snippet);
	}

	// @protocol MapboxWrapperCallback
	/*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/

	interface IMapboxWrapperCallback { }

	[Protocol(Name = "_TtP13MapboxInterop21MapboxWrapperCallback_"), Model]
	interface MapboxWrapperCallback
	{
		// @required -(void)markerClickedWithTitle:(NSString * _Nonnull)title;
		[Abstract]
		[Export("markerClickedWithTitle:")]
		void MarkerClicked(string title);

		// @required -(void)mapReady;
		[Abstract]
		[Export("mapReady")]
		void MapReady();
	}
}
