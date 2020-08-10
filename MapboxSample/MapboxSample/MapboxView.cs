using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapboxSample
{
	public interface IMapboxView
	{
		void AddMarker(double lat, double lng, string title, string snippet);
	}

	public class MapboxView : View
	{

		public event EventHandler MapReady;
		public event EventHandler<string> MarkerTapped;

		public string AccessToken { get; set; }

		public double InitialLatitude { get; set; } = 0d;
		public double InitialLongitude { get; set; } = 0d;
		public int InitialZoom { get; set; } = 12;

		public void AddMarker(double lat, double lng, string title, string snippet)
		{
			// Get renderer as IMapboxView and call AddMarker on it

		}

		public void RaiseMarkerTapped(string title)
			=> MarkerTapped?.Invoke(this, title);

		public void RaiseMapReady()
			=> MapReady?.Invoke(this, new EventArgs());

	}
}
