using Acr.UserDialogs;
using Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Midia_Indoo.Banco.Contratos;
using Midia_Indoo.Helps;
using Midia_Indoo.Helps.IDependecyService;
using Midia_Indoo.Repository;
using Midia_Indoo.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Midia_Indoo.ViewModels
{
    public class VideosViewModel : ViewModelBase
    {

        private Player _Player;
        public Player Player
        {
            get { return _Player; }
            set { SetProperty(ref _Player, value); }
        }
        private ObservableCollection<Videos> _ListVideos;
        public ObservableCollection<Videos> ListVideos
        {
            get { return _ListVideos; }
            set { SetProperty(ref _ListVideos, value); }
        }
        public string NomeVideo { get; set; }

        private ImageSource _Image;
        public ImageSource Image
        {
            get { return _Image; }
            set { SetProperty(ref _Image, value); }
        }
        public DelegateCommand AddVideoCommand { get; set; }
        public DelegateCommand UpdadateVideoCommand { get; set; }
        public DelegateCommand RelatorioVideoCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Videos> DeleteCommand { get; set; }
        public IVideosRepositorio VideosRepository { get; }
        public IPageDialogService PageDialogService { get; }
        public IVideosRepository VideoSerivce { get; }
        HubConnection hubConnection;
        public VideosViewModel(INavigationService navigationService,
            IVideosRepositorio videosRepository, IPageDialogService pageDialogService,
            IVideosRepository videoSerivce)
            : base(navigationService, pageDialogService)
        {

            VideosRepository = videosRepository;
            PageDialogService = pageDialogService;
            VideoSerivce = videoSerivce;
            ListVideos = new ObservableCollection<Videos>();
            DoRealTimeSuff();
            AddVideoCommand = new DelegateCommand(async () => await AddVideo());
            RelatorioVideoCommand = new DelegateCommand(ChamarTelaRelatorio);
            UpdadateVideoCommand = new DelegateCommand(async () => await EnviarApi());
            DeleteCommand = new DelegateCommand<Videos>(async (Videos) => await DeleteVideo(Videos));
            RefreshCommand = new DelegateCommand(async () => await PegarVideos(Player.Codigo));
        }
        async private void DoRealTimeSuff()
        {
            SignalRChatSetup();
            await SignalRConnect();
        }
        private void SignalRChatSetup()
        {
            //var ip = "localhost";
            hubConnection = new HubConnectionBuilder().WithUrl("http://191.252.64.6/mi/MsgHub").
            //hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.18.79:5000/MsgHub").
                WithAutomaticReconnect()
                .Build();


        }

        async Task SignalRConnect()
        {
            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                // Connection failed. Fail graciously.
                await PageDialogService.DisplayAlertAsync("Erro!", ex.Message, "OK");
            }
        }
        async Task SignalRDisconnect()
        {
            try
            {
                //await hubConnection.dis();
            }
            catch (Exception ex)
            {
                // Connection failed. Fail graciously.
                await PageDialogService.DisplayAlertAsync("Erro!", ex.Message, "OK");
            }
        }
        async Task SignalRSendVideo(Guid GuidVideo)
        {
            try
            {
                //await hubConnection.StartAsync();
                var x = hubConnection.State;
                if (hubConnection.State == HubConnectionState.Disconnected)
                    await SignalRConnect();


                await hubConnection.InvokeAsync("SendVideo", GuidVideo);
            }
            catch (Exception ex)
            {
                // Send failed. Fail graciously.
                await PageDialogService.DisplayAlertAsync("Erro!", ex.Message, "OK");

            }
        }
        private async Task DeleteVideo(Videos video)
        {

            using (var Dialog = UserDialogs.Instance.Loading($"Excluindo Video {video.Nome}", null, null, true, MaskType.Black))
            {
                if (await PageDialogService.DisplayAlertAsync("Alerta!", "Desejá deletar este Video?", "Sim", "Não"))
                {
                    try
                    {
                        var request = await VideoSerivce.PostDeleteAsync(video);
                        VideosRepository.Delete(video);
                        if (request.IsSuccess)
                        {
                            await CarregaVideos();
                            await SignalRSendVideo(video.VideoGuid);
                        }
                        else
                            await PageDialogService.DisplayAlertAsync("Erro!", request.Error, "Ok");

                    }
                    catch (Exception ex)
                    {
                        var err = ex.Message;
                        await PageDialogService.DisplayAlertAsync("Error!", err, "OK");
                    }


                }
            }
        }
        private async void ChamarTelaRelatorio()
        {
            var navigationParams = new NavigationParameters { { "_PlayerKey", Player } };

            await SafeNavigateAsync(nameof(RelatorioVideosPage), navigationParams);
        }
        private async Task CarregaVideos()
        {
            await Task.Run(() =>
             {

                 var _video = VideosRepository.GetByIdFather(Player.Codigo);
                 if (_video != null)
                     ListVideos = new ObservableCollection<Videos>(_video);
             });
        }
        private async Task AddVideo()
        {
            if (await PegarVideo())
            {
                var _video = new Videos
                {
                    Date = DateTime.Now,
                    Nome = NomeVideo,
                    PlayerID = Player.Codigo,
                    VideoGuid = Guid.NewGuid()
                };


                VideosRepository.Add(_video);
                await Task.Delay(200);

                await CarregaVideos();
            }
        }
        private async Task<bool> PegarVideo()
        {
            NomeVideo = "";
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Selecione o video",
                    FileTypes = FilePickerFileType.Videos
                });
                if (result != null)
                {
                    NomeVideo = result.FileName;
                    var Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("mp4", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("avi", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("m4v", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("mov", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        // VID-20211001-WA0006.mp4
                        var _video = VideosRepository.GetByIdFather(Player.Codigo).FirstOrDefault(x => x.Nome == NomeVideo);
                        var RootLocalPath = Xamarin.Forms.DependencyService.Get<IDeviceSdkService>().GetRootLocalPath();
                        var DiretorioVideos = $"{RootLocalPath}/Videos/";
                        if (File.Exists(DiretorioVideos + result.FileName) && _video != null)
                        {
                            await PageDialogService.DisplayAlertAsync("Alerta", "Esse video já esta cadastrado nesse player! ", "OK");
                            return false;
                        }
                        if (_video == null)
                        {
                            if (!File.Exists(DiretorioVideos + result.FileName))
                                File.Copy(result.FullPath, DiretorioVideos);

                            return true;
                        }


                        return false;

                        //var vid = ImageSource.FromStream(() => stream);

                        //var Byte = await ConverteStreamToByteArray(stream);


                        //    Image = Xamarin.Forms.ImageSource.FromStream(
                        //() => new MemoryStream(Convert.FromBase64String(Base64Img)));
                    }
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                await PageDialogService.DisplayAlertAsync("Erro!", err, "Ok");
                return false;
            }
        }
        public async Task EnviarApi()
        {
            try
            {
                if (await MsgErrInterntOK("", "Sem Conexão com Internet!"))
                    return;


                var RootLocalPath = Xamarin.Forms.DependencyService.Get<IDeviceSdkService>().GetRootLocalPath();
                var DiretorioVideos = $"{RootLocalPath}/Videos/";
                UserDialogs.Instance.ShowLoading($"Carregado...", maskType: MaskType.Black);

                foreach (var video in ListVideos)
                {

                    if (File.Exists(DiretorioVideos + video.Nome))
                    {
                        var request = await VideoSerivce.SeachVideoAsync(video.VideoGuid);
                        if (request.Data == false)
                        {
                            UserDialogs.Instance.HideLoading();
                            using (var Dialog = UserDialogs.Instance.Loading($"Enviado\n {video.Nome}", null, null, true, MaskType.Black))
                            {
                                //UserDialogs.Instance.ShowLoading($"Enviado {video.Nome}", maskType: MaskType.Black);
                                var _video = File.OpenRead(DiretorioVideos + video.Nome);
                                var Byte = await ConverteStreamToByteArray(_video);
                                video.UriByte = Byte;
                                var _req = await VideoSerivce.PostAsync(video);
                                if (_req.StatusCode == 200)
                                {
                                    await SignalRSendVideo(video.VideoGuid);
                                    await UserDialogs.Instance.AlertAsync("Video Enviado", "Sucesso!", "OK");
                                }
                                else
                                {
                                    await PageDialogService.DisplayAlertAsync($"Erro! = {request.StatusCode.ToString()}", "", "Ok");
                                    
                                    return;
                                }
                                //var base64 = Convert.ToBase64String(Byte);
                                //video.UriBase64 = base64;
                                //UserDialogs.Instance.HideLoading();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                await PageDialogService.DisplayAlertAsync("Erro!", ex.Message, "OK");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();

            }
        }
        public async Task<byte[]> ConverteStreamToByteArray(Stream stream)
        {
            var byteArray = new byte[16 * 1024];
            using (var mStream = new MemoryStream())
            {
                int bit;
                while ((bit = await stream.ReadAsync(byteArray, 0, byteArray.Length)) > 0) await mStream.WriteAsync(byteArray, 0, bit);
                return mStream.ToArray();
            }
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("_PlayerKey"))
            {
                Player = parameters.GetValue<Player>("_PlayerKey");
                await CarregaVideos();
                await PegarVideos(Player.Codigo);
                UserDialogs.Instance.HideLoading();
            }
        }
        private async Task PegarVideos(int codigo)
        {
            if (ListVideos.Count == 0)
            {
                IsRefreshing = true;
                var result = await VideoSerivce.GetPartVideo(codigo);
                if (result.IsSuccess)
                {
                    if (result.Data.Count > 0)
                    {
                        foreach (var video in result.Data)
                        {
                            VideosRepository.Add(video);
                        }
                        await CarregaVideos();
                        IsRefreshing = false;
                    }
                }
            }

        }
    }
}
