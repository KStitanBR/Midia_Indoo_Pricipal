using Acr.UserDialogs;
using Domain.Models;
using Midia_Indoo.Helps;
using Midia_Indoo.Helps.IDependecyService;
using Midia_Indoo.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Midia_Indoo.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {

        private Usuario _UsuarioLogado;
        public Usuario UsuarioLogado
        {
            get { return _UsuarioLogado; }
            set { SetProperty(ref _UsuarioLogado, value); }
        }
        public DelegateCommand<string> GoNavigateCommand { get; set; }
        public DelegateCommand CadastrarCommand { get; set; }
        public DelegateCommand MensagensCommand { get; set; }
        public DelegateCommand LogOutCommand { get; set; }
        public IPageDialogService PageDialogService { get; }

        public MenuViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            GoNavigateCommand = new DelegateCommand<string>(Navigate);
            CadastrarCommand = new DelegateCommand(ChamarTelaCadastro);
            LogOutCommand = new DelegateCommand(ToGoOut);
            PageDialogService = pageDialogService;
        }

        private async void ToGoOut()
        {
            if (await PageDialogService.DisplayAlertAsync("Sair", "Desejá sair da conta?", "Sim", "Não"))
            {
                Preferences.Remove("Login_key");
                Preferences.Remove("Senha_key");
                await SafeNavigateAsync(nameof(LoginPage));
            }
        }

        private async void Navigate(string url)
        {
            //if (!Directory.Exists(Env.RootLocalPath))
            //{
            //    //Directory.SetCurrentDirectory(Env.RootLocalPath);
            //    Directory.CreateDirectory(Env.RootLocalPath);
            //}
            //if (!Directory.Exists(Env.DiretorioVideos))
            //{
            //    //Directory.SetCurrentDirectory(Env.RootLocalPath);
            //    Directory.CreateDirectory(Env.DiretorioVideos);
            //}
            //using (var Dialog = UserDialogs.Instance.Loading("Carregando...", null, null, true, MaskType.Gradient))
            //{
            UserDialogs.Instance.ShowLoading("Carregando...");
            var navigationParams = new NavigationParameters { { "_UsuarioKey", UsuarioLogado } };
            await SafeNavigateAsync(url, navigationParams);
            //}
        }
        private void ChamarTelaCadastro()
        {

            SafeNavigateAsync(nameof(CadastroPage));
        }
        public void CreateFolders()
        {
            var RootLocalPath = Xamarin.Forms.DependencyService.Get<IDeviceSdkService>().GetRootLocalPath();
            if (!Directory.Exists(RootLocalPath))
                Directory.CreateDirectory(RootLocalPath);
            var DiretorioVideos = $"{RootLocalPath}/Videos/";
            if (!Directory.Exists(DiretorioVideos))
                Directory.CreateDirectory(DiretorioVideos);
            var _PathRelatorio = $"{RootLocalPath}/Relatorios/";
            if (!Directory.Exists(_PathRelatorio))
                Directory.CreateDirectory(_PathRelatorio);
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("_UsuarioKey"))
            {
                UsuarioLogado = parameters.GetValue<Usuario>("_UsuarioKey");
                CreateFolders();
            }
        }
    }
}
