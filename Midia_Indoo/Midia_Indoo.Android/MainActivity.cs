using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using System;
using Xamarin.Essentials;
using static Java.Util.ResourceBundle;

namespace Midia_Indoo.Droid
{
    [Activity(Label = "Gerenciador", Theme = "@style/MainTheme", Icon = "@mipmap/ic_launcher",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation |
        ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [assembly: Dependency(typeof(MyCoffeeApp.Droid.Environment))]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            UserDialogs.Init(this);
            await Permissions.RequestAsync<ReadWriteStoragePermission>();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }


        //public class Environment : IEnvironment
        //{
        //    public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        //    {
        //        if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
        //            return;

        //        var activity = Platform.CurrentActivity;
        //        var window = activity.Window;
        //        window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
        //        window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
        //        window.SetStatusBarColor(color.ToPlatformColor());

        //        if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
        //        {
        //            var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
        //            window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
        //        }
        //    }
        //}
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

