using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Midia_Indoo.Helps
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                //default
                case 0:
                    App.Current.UserAppTheme = Xamarin.Forms.OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    App.Current.UserAppTheme = Xamarin.Forms.OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    App.Current.UserAppTheme = Xamarin.Forms.OSAppTheme.Dark;
                    break;
            }

            var nav = App.Current.MainPage as Xamarin.Forms.NavigationPage;

            var e = Xamarin.Forms.DependencyService.Get<IEnvironment>();
            if (App.Current.RequestedTheme == Xamarin.Forms.OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.Black, false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.Black;
                    nav.BarTextColor = Color.White;
                }
            }
            else
            {
                e?.SetStatusBarColor(Color.White, true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.White;
                    nav.BarTextColor = Color.Black;
                }
            }


        }
    }
}
