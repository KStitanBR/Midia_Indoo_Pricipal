using Domain.Services;
using Midia_Indoo.Banco.Repositorios;
using Midia_Indoo.Helps;
using Midia_Indoo.Repository;
using Midia_Indoo.ViewModels;
using Midia_Indoo.Views;
using MonkeyCache.SQLite;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Midia_Indoo
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
          
        }
        private async Task CheckLoggedUser(Banco.Contratos.IUsuarioRepositorio usuarioRepositorio)
        {
            if (Preferences.ContainsKey("Login_key") && Preferences.ContainsKey("Senha_key"))
            {
                var user = usuarioRepositorio.GetByUser(Preferences.Get("Login_key", "default_value"), Preferences.Get("Senha_key", "default_value"));
                var navigationParams = new NavigationParameters { { "_UsuarioKey", user } };
                await NavigationService.NavigateAsync($"NavigationPage/{nameof(MenuPage)}", navigationParams);

            }
            else
            {
                await NavigationService.NavigateAsync($"NavigationPage/{nameof(LoginPage)}");
            }
        }
        protected async override void OnInitialized()
        {
            InitializeComponent();
            Barrel.ApplicationId = "NewsfeedAppId";

            await CheckLoggedUser(new UsuarioRepository());

            await CheckAndRequestPermission();
        }

        private async Task<PermissionStatus> CheckAndRequestPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            return status;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<CadastroPage, CadastroViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuViewModel>();
            containerRegistry.RegisterForNavigation<PlayersPage, PlayersViewModel>();
            containerRegistry.RegisterForNavigation<VideosPage, VideosViewModel>();
            containerRegistry.RegisterForNavigation<RelatorioVideosPage, RelatorioVideosViewModel>();
            containerRegistry.RegisterForNavigation<MensagensPage, MensagensViewModel>();

            // Serviços Api
            containerRegistry.Register<IUsuarioRepository, UsuarioService>();
            containerRegistry.Register<IPlayerRepository, PlayerService>();
            containerRegistry.Register<IMensagemRepository, MensagemServices>();
            containerRegistry.Register<IVideosRepository, VideoService>();
            containerRegistry.Register<IPlayeReqRepository, PlayeReqService>();
            containerRegistry.Register<IVideoReqRepository, VideoReqService>();

            // Repository SqLite
            containerRegistry.Register<Banco.Contratos.IMensagemRepositorio,    Banco.Repositorios.MensagemRepository>();
            containerRegistry.Register<Banco.Contratos.IPlayerRepositorio,    Banco.Repositorios.PlayerRepository>();
            containerRegistry.Register<Banco.Contratos.IUsuarioRepositorio, Banco.Repositorios.UsuarioRepository>();
            containerRegistry.Register<Banco.Contratos.IVideosRepositorio, Banco.Repositorios.VideosRepository>();
        }
    }
}
