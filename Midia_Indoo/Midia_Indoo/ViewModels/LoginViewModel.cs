using Acr.UserDialogs;
using Midia_Indoo.Banco.Contratos;
using Midia_Indoo.Helps;
using Midia_Indoo.Helps.IDependecyService;
using Midia_Indoo.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace Midia_Indoo.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public DelegateCommand GoMenuCommand { get; set; }
        public IUsuarioRepositorio UsuarioRepositorio { get; }

        public LoginViewModel(INavigationService navigationService, IUsuarioRepositorio usuarioRepositorio, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            UsuarioRepositorio = usuarioRepositorio;
            GoMenuCommand = new DelegateCommand(GoMenu);
        }

        private async void GoMenu()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Senha))
            {
                var _usuario = UsuarioRepositorio.GetByUser(Email, Senha);

                if (_usuario != null)
                {
                    Busy = false;
                    ColoErr = Xamarin.Forms.Color.White;
                    using (var Dialog = UserDialogs.Instance.Loading("Entrando..", null, null, true, MaskType.Black))
                    {
                        await Task.Delay(2000);
                        Preferences.Set("Login_key", Email);
                        Preferences.Set("Senha_key", Senha);

                        var navigationParams = new NavigationParameters { { "_UsuarioKey", _usuario } };
                        await SafeNavigateAsync(nameof(MenuPage), navigationParams);
                    }
                }
                Error("O campo E-mail ou Senha incorreta.\n Tente novamente!");
            }
            ColoErr = Xamarin.Forms.Color.Red;
            //Error(" Certifique-se de que todos os campos estejam preenchidos.");
        }

        public void Error(string msg)
        {
            MsgErr = msg;
            Busy = true;
            ColoErr = Color.Red;
            Senha = "";
        }

        //public void CreateFolders()
        //{
        //    var RootLocalPath = Xamarin.Forms.DependencyService.Get<IDeviceSdkService>().GetRootLocalPath();
        //    if (!Directory.Exists(RootLocalPath));
        //        Directory.CreateDirectory(RootLocalPath);
        //    var DiretorioVideos = $"{RootLocalPath}/Videos/";
        //    if (!Directory.Exists(DiretorioVideos))
        //        Directory.CreateDirectory(DiretorioVideos);
        //}

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }

        private string _Senha;
        public string Senha
        {
            get { return _Senha; }
            set { SetProperty(ref _Senha, value); }
        }

        private Color _ColoErr;
        public Color ColoErr
        {
            get { return _ColoErr; }
            set { SetProperty(ref _ColoErr, value); }
        }

        private string _MsgErr;
        public string MsgErr
        {
            get { return _MsgErr; }
            set { SetProperty(ref _MsgErr, value); }
        }
    }
}
