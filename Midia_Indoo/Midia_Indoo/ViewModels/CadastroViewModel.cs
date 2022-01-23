using Domain.Models;
using Midia_Indoo.Banco.Contratos;
using Midia_Indoo.Repository;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.ViewModels
{
    public class CadastroViewModel : ViewModelBase
    {


        private string _Nome;
        public string Nome
        {
            get { return _Nome; }
            set { SetProperty(ref _Nome, value); }
        }

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

        private string _Msg;
        public string Msg
        {
            get { return _Msg; }
            set { SetProperty(ref _Msg, value); }
        }

        public DelegateCommand AddUsuarioCommand { get; set; }
        public IPageDialogService DialogService { get; }
        public IUsuarioRepository UsuarioRepository { get; }

        public CadastroViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            IUsuarioRepository usuarioRepository) : base(navigationService, dialogService)
        {
            AddUsuarioCommand = new DelegateCommand(NovoUsuario);
            DialogService = dialogService;
            UsuarioRepository = usuarioRepository;
        }

        private async void NovoUsuario()
        {
                if (VerificarCampos())
            {
                var _novoUsuario = new Usuario
                {
                    Nome = this.Nome,
                    Email = this.Email,
                    Senha = this.Senha,
                    UsuarioGuid = Guid.NewGuid()

                };
                var _request = await UsuarioRepository.PostAsync(_novoUsuario);

                if (_request.IsSuccess)
                    await DialogService.DisplayAlertAsync("Sucesso!", $"Usuario cadastrado com Sucesso! ", "OK");

                else
                    await DialogService.DisplayAlertAsync("Erro!", $"{_request.Error}! ", "OK");
            }
        }

        public bool VerificarCampos()
        {
            if (!string.IsNullOrWhiteSpace(Nome) ||
                !string.IsNullOrWhiteSpace(Email) ||
                !string.IsNullOrWhiteSpace(Senha))
                return true;

            return false;
        }
    }
}
