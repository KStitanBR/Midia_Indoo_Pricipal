using Midia_Indoo.Helps.IDependecyService;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(Midia_Indoo.Droid.DependencyServicess.DeviceSdkService))]

namespace Midia_Indoo.Droid.DependencyServicess
{
    public class DeviceSdkService : IDeviceSdkService
    {

        public string GetRootLocalPath() => VersionSdk11() ? $"{FileSystem.AppDataDirectory}/MediaIndoo" : "./sdcard/MediaIndoo";
        public bool VersionSdk11() => (int)Android.OS.Build.VERSION.SdkInt > 29 ? true : false;

    }
}