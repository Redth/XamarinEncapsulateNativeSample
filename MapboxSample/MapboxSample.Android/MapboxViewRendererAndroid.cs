using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Codes.Redth.Mapboxinterop;
using MapboxSample;
using MapboxSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MapboxView), typeof(MapboxViewRendererAndroid))]

namespace MapboxSample.Droid
{
	public class MapboxViewRendererAndroid : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<MapboxView, Android.Views.View>, IMapboxWrapperCallback
	{
		public MapboxViewRendererAndroid(Context context)
			: base(context)
		{
		}

		public static Dictionary<string, MapboxWrapper> Instances = new Dictionary<string, MapboxWrapper>();

		MapboxWrapper mapbox = null;
		readonly string instanceId = Guid.NewGuid().ToString();

		protected override void OnElementChanged(ElementChangedEventArgs<MapboxView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				Element.MarkerAdded -= Element_MarkerAdded;

				// Unsubscribe from event handlers and cleanup any resources
				if (Instances.ContainsKey(instanceId))
					Instances.Remove(instanceId);
			}

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					mapbox = new MapboxWrapper(this.Context as Activity, this);
					mapbox.Init(Element.AccessToken);
					mapbox.OnCreate(null, new Java.Lang.Double(Element.InitialLatitude), new Java.Lang.Double(Element.InitialLongitude), new Java.Lang.Integer(Element.InitialZoom));

					// Add our instance to the static dictionary for mainactivity lifecycle events
					Instances[instanceId] = mapbox;

					SetNativeControl(mapbox.View);
				}

				Element.MarkerAdded += Element_MarkerAdded;
			}
		}

		void Element_MarkerAdded(object sender, MapMarker e)
			=> mapbox?.AddMarker(e.Latitude, e.Longitude, e.Title, e.Snippet);

		public void MapReady()
			=> Element?.RaiseMapReady();

		public void MarkerClicked(string title)
			=> Element?.RaiseMarkerTapped(title);

		public void AddMarker(double lat, double lng, string title, string snippet)
			=> mapbox.AddMarker(lat, lng, title, snippet);
	}
}