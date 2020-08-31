using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MapboxSample
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		int markerCount = 1;

		private void Button_Clicked(object sender, EventArgs e)
		{
			var rnd = new Random();

			var variation = (rnd.Next(0, 10) / 10d);

			if (rnd.Next(0, 1) == 1)
				variation *= -1;

			var lat = 42.09221583651157 + variation;
			var lng = -82.4735913540103 + variation;
			this.map.AddMarker(lat, lng, $"Marker {markerCount++}", "Details");
		}

		async void map_MarkerTapped(object sender, string e)
			=> await DisplayAlert("Marker Tapped", $"Marker '{e}' was tapped.", "OK");
	}
}
