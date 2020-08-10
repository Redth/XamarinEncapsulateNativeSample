using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MapboxSample.Droid
{
    [Activity(Label = "MapboxSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

		protected override void OnResume()
		{
			base.OnResume();

            foreach (var instance in MapboxViewRendererAndroid.Instances.Values)
                instance.OnResume();
        }

        protected override void OnStart()
        {
            base.OnStart();
            foreach (var instance in MapboxViewRendererAndroid.Instances.Values)
                instance.OnStart();
        }

		protected override void OnPause()
		{
			base.OnPause();
            foreach (var instance in MapboxViewRendererAndroid.Instances.Values)
                instance.OnPause();
        }

		protected override void OnStop()
		{
			base.OnStop();
            foreach (var instance in MapboxViewRendererAndroid.Instances.Values)
                instance.OnStop();

        }

	}
}