using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapboxSample;
using MapboxSample.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MapboxViewRendereriOS), typeof(MapboxView))]


namespace MapboxSample.iOS
{
	class MapboxViewRendereriOS : Xamarin.Forms.Platform.iOS.ViewRenderer<MapboxView, UIView>
	{
		public static Dictionary<string, MapboxWrapper> Instances = new Dictionary<string, MapboxWrapper>();

		MapboxWrapper mapbox = null;
		readonly string instanceId = Guid.NewGuid().ToString();


		protected override void OnElementChanged(ElementChangedEventArgs<MapboxView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources
				if (Instances.ContainsKey(instanceId))
					Instances.Remove(instanceId);
			}

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					mapbox = new MapboxWrapper(this);
					mapbox.OnCreate(null, Element.InitialLatitude, Element.InitialLongitude, Element.InitialZoom);

					// Add our instance to the static dictionary for mainactivity lifecycle events
					Instances[instanceId] = mapbox;

					SetNativeControl(mapbox.View);
				}
			}
		}
	}
}