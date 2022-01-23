using Acr.UserDialogs;
using Domain.Helps;
using Domain.Models;
using Midia_Indoo.Banco.Contratos;
using Midia_Indoo.Repository;
using Midia_Indoo.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Midia_Indoo.ViewModels
{
    public class PlayersViewModel : ViewModelBase
    {


        private ObservableCollection<Player> _Players;
        public ObservableCollection<Player> Players
        {
            get { return _Players; }
            set { SetProperty(ref _Players, value); }
        }

        private ObservableCollection<UltimoAcessoPlayer> _UltimoAcessos;
        public ObservableCollection<UltimoAcessoPlayer> UltimoAcessos
        {
            get { return _UltimoAcessos; }
            set { SetProperty(ref _UltimoAcessos, value); }
        }
        private Usuario _UsuarioLogado;
        public Usuario UsuarioLogado
        {
            get { return _UsuarioLogado; }
            set { SetProperty(ref _UsuarioLogado, value); }
        }
        public DelegateCommand AddCommaand { get; set; }
        public DelegateCommand<Player> GoVideosCommand { get; set; }
        public DelegateCommand<Player> DeleteCommand { get; set; }

        public IPlayerRepositorio PlayerRepository { get; }
        public IPlayeReqRepository PlayeReqService { get; }
        public IPlayerRepository PlayerService { get; }

        public PlayersViewModel(INavigationService navigationService,
                                IPlayerRepositorio playerRepositorio,
                                IPlayeReqRepository playeReqService,
                                IPlayerRepository playerService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            PlayerRepository = playerRepositorio;
            PlayeReqService = playeReqService;
            PlayerService = playerService;
            Players = new ObservableCollection<Player>();
            AddCommaand = new DelegateCommand(AdicionarNovoPlayer);
            GoVideosCommand = new DelegateCommand<Player>(ChamarTelaListVideos);
            DeleteCommand = new DelegateCommand<Player>(DeletarPlayer);
        }

        private async void DeletarPlayer(Player obj)
        {
            if (await PageDialogService.DisplayAlertAsync("Alerta!", "Desejá deletar este Player?", "Sim", "Não"))
            {
                //PlayerRepository.Delete(obj);
                var request = await PlayerService.PostDeleteAsync(obj);
                if (request.IsSuccess)
                    await CarregaPlayers();
            }
        }

        private async Task CarregaPlayers()
        {
            Players.Clear();
            if (await MsgErrInterntOK("", "Sem Conexão com Internet!"))
                return;

            var resultPlayer = await PlayerService.GetAllPlayersId(UsuarioLogado.Codigo);
            if (resultPlayer.IsSuccess)

                Players = new ObservableCollection<Player>(resultPlayer.Data);
        }


        private async void ChamarTelaListVideos(Player _player)
        {

            //using (var Dialog = UserDialogs.Instance.Loading("Carregando Videos...", null, null, true, MaskType.Black))
            //{
                UserDialogs.Instance.ShowLoading("Carregando Videos...");
                if (_player != null)
            {
                var navigationParams = new NavigationParameters { { "_PlayerKey", _player } };
                await SafeNavigateAsync(nameof(VideosPage), navigationParams);
            }
            //}
        }
        // colocar Nome do usuario 
        private async void AdicionarNovoPlayer()
        {
            var _Nome = await App.Current.MainPage.DisplayPromptAsync("Novo Player!", "", "OK", "Cancelar", "Digite o nome do player!");
            if (!string.IsNullOrWhiteSpace(_Nome))
            {
                var _player = new Player
                {
                    Nome = _Nome,
                    UsuarioID = UsuarioLogado.Codigo,
                    PlayerGuid = Guid.NewGuid()
                };
                await PlayerService.PostAsync(_player);

                await Task.Delay(300);
                await CarregaPlayers();
            }
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("_UsuarioKey"))
            {
                UsuarioLogado = parameters.GetValue<Usuario>("_UsuarioKey");
                await CarregaPlayers();
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
