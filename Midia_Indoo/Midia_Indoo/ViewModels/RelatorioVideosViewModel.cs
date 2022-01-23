using Domain.Models;
using Midia_Indoo.Helps.IDependecyService;
using Midia_Indoo.Repository;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Midia_Indoo.ViewModels
{
    public class RelatorioVideosViewModel : ViewModelBase
    {

        private ObservableCollection<UltimoAcessoPlayer> _UltimoAcessoPlayers;
        public ObservableCollection<UltimoAcessoPlayer> UltimoAcessoPlayers
        {
            get { return _UltimoAcessoPlayers; }
            set { SetProperty(ref _UltimoAcessoPlayers, value); }
        }


        private ObservableCollection<VideoReq> _Reqs;
        public ObservableCollection<VideoReq> Reqs
        {
            get { return _Reqs; }
            set { SetProperty(ref _Reqs, value); }
        }
        private Player _Player;
        public Player Player
        {
            get { return _Player; }
            set { SetProperty(ref _Player, value); }
        }

        public IVideoReqRepository VideoReqRepository { get; }

        public DelegateCommand BaixarRelatorioCommand { get; set; }
        public RelatorioVideosViewModel(INavigationService navigationService,
            IPageDialogService pageDialogService,
            IVideoReqRepository videoReqRepository
           ) : base(navigationService, pageDialogService)
        {
            VideoReqRepository = videoReqRepository;
            BaixarRelatorioCommand = new DelegateCommand(async () => GerarRelatorio());
        }

        private async Task GerarRelatorio()
        {

            if (await PageDialogService.DisplayAlertAsync("Gerar Relatorio", "Desejá gerar relatorio", "Sim", "Não"))
            {


                //var _relatorio = File.CreateText(PegarNome());


                using (StreamWriter writer = new StreamWriter(PegarNome(), true))
                {
                    foreach (var item in Reqs)
                    {
                        writer.WriteLine(EscreverRelatorio(item));
                    }

                    if (await PageDialogService.DisplayAlertAsync($"Relatorio {Player.Nome}", "Desejá abrir?", "Sim", "Não"))
                    {
                        await Launcher.OpenAsync(new OpenFileRequest
                        {
                            File = new ReadOnlyFile(Path.Combine(PegarNome()))
                        });
                    }
                }
                //foreach (var item in Reqs)
                //{

                //    _relatorio.WriteLine(EscreverRelatorio(item));
                //}

                //if(await PageDialogService.DisplayAlertAsync($"Relatorio {Player.Nome}", "Desejá abrir?", "Sim", "Não"))
                //{
                //    await Launcher.OpenAsync(new OpenFileRequest
                //    {
                //        File = new ReadOnlyFile(Path.Combine(PegarNome()))
                //    });
                //}
            }

        }

        private string EscreverRelatorio(VideoReq videoReq)
        {
            return $"{Player.Nome} -- {videoReq.Date.ToString()} -- {videoReq.NomeVideo}\n ";
        }
        private string PegarNome()
        {
            //var nome = await App.Current.MainPage.DisplayPromptAsync("Relatorio!", "", "OK", "Cancelar", "Digite o nome do relatorio!");
            var RootLocalPath = Xamarin.Forms.DependencyService.Get<IDeviceSdkService>().GetRootLocalPath();
            return $"{RootLocalPath}/Relatorios/{Player.Nome}.txt";
        }

        private async Task CarregarRelatorio(int codigo)
        {
            var request = await VideoReqRepository.GetAllVideoReqsId(codigo);
            if (request.IsSuccess)
            {
                var _list = request.Data.Where(x => x.Date <= DateTime.Now.AddDays(7)).Select(d => d).ToList();

                Reqs = new ObservableCollection<VideoReq>(_list);
            }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("_PlayerKey"))
            {
                Player = parameters.GetValue<Player>("_PlayerKey");
                await CarregarRelatorio(Player.Codigo);
                //ListVideos = parameters.GetValue<ObservableCollection<Videos>>("_VideosKey");
            }
        }
    }
}