using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapboxInterop;
using MapboxSample;
using MapboxSample.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MapboxView), typeof(MapboxViewRendereriOS))]

namespace MapboxSample.iOS
{
	class MapboxViewRendereriOS : Xamarin.Forms.Platform.iOS.ViewRenderer<MapboxView, UIView>, IMapboxWrapperCallback
	{
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
					mapbox = new MapboxWrapper(this.Frame, this);
					mapbox.Init();

					// Add our instance to the static dictionary for mainactivity lifecycle events
					Instances[instanceId] = mapbox;

					SetNativeControl(mapbox.View);
				}

				Element.MarkerAdded += Element_MarkerAdded;
			}
		}

		void Element_MarkerAdded(object sender, MapMarker e)
			=> mapbox?.AddMarker(e.Latitude, e.Longitude, e.Title, e.Snippet);

		public void MarkerClicked(string title)
			=> Element?.RaiseMarkerTapped(title);

		public void MapReady()
			=> Element?.RaiseMapReady();
	}
}