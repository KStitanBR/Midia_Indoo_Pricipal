using Acr.UserDialogs;
using Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Midia_Indoo.Banco.Contratos;
using Midia_Indoo.Repository;
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
    public class MensagensViewModel : ViewModelBase
    {
        private Usuario _UsuarioLogado;
        public Usuario UsuarioLogado
        {
            get { return _UsuarioLogado; }
            set { SetProperty(ref _UsuarioLogado, value); }
        }

        private ObservableCollection<Mensagem> _mensagens;
        public ObservableCollection<Mensagem> mensagens
        {
            get { return _mensagens; }
            set { SetProperty(ref _mensagens, value); }
        }

        public IMensagemRepositorio MensagemRepositorio { get; }
        public IMensagemRepository MensagemService { get; }
        public IPageDialogService PageDialogService { get; }
        public DelegateCommand AddMensagemCommand { get; set; }
        public DelegateCommand EnviarMsgCommand { get; set; }
        public DelegateCommand<Mensagem> DeleteCommand { get; set; }
        HubConnection hubConnection;
        public MensagensViewModel(INavigationService navigationService,
            IMensagemRepositorio mensagemRepositorio,
            IMensagemRepository mensagemService,
            IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            mensagens = new ObservableCollection<Mensagem>();
            MensagemRepositorio = mensagemRepositorio;
            MensagemService = mensagemService;
            PageDialogService = pageDialogService;
            EnviarMsgCommand = new DelegateCommand(async () => await EnviarMensagemApi());
            AddMensagemCommand = new DelegateCommand(async () => await AddMensagem());
            DeleteCommand = new DelegateCommand<Mensagem>(async (Mensagem) => await DeletarMsg(Mensagem));

            //DoRealTimeSuff();
            //Connect();
        }

        async private void DoRealTimeSuff()
        {
            SignalRChatSetup();
            await SignalRConnect();
        }
        private void SignalRChatSetup()
        {
            //var ip = "localhost";
            //hubConnection = new HubConnectionBuilder().WithUrl("http://191.252.64.6/mi/MsgHub").Build();
            //hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.18.79:5000/MsgHub").Build();

            //hubConnection.On<int, List<Mensagem>>("ReceiveMessage", (codigo, message) =>
            //{
            //    var receivedMessage = message;
            //    var x = receivedMessage;
            //    //this.MessageHolder.Text += receivedMessage + "\n";
            //});

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
                await PageDialogService.DisplayAlertAsync("Error!", ex.Message, "OK");
            }
        }
        async Task SignalRSendMessage(int codigo, Mensagem message)
        {
            try
            {
                //await hubConnection.StartAsync();
                await hubConnection.InvokeAsync("SendMessage", codigo, message);
            }
            catch (Exception ex)
            {
                // Send failed. Fail graciously.
                await PageDialogService.DisplayAlertAsync("Error!", ex.Message, "OK");

            }
        }
        private async Task DeletarMsg(Mensagem obj)
        {
            try
            {
                if (await PageDialogService.DisplayAlertAsync("Alerta!", "Desejá deletar essa menssagem?", "Sim", "Não"))
                {

                    MensagemRepositorio.Delete(obj);
                    var _request = await MensagemService.PostDeleteAsync(obj);

                    //if (_request.IsSuccess)
                     CarregarMensagens();
                    //await SignalRSendMessage(UsuarioLogado.Codigo, obj);
                }
            }
            catch (Exception)
            {

                await PageDialogService.DisplayAlertAsync("Error!", "Ocorreu um erro ao tentar deletar essa menagem!", "OK");
            }

        }

        private async Task AddMensagem()
        {
            var _msg = await App.Current.MainPage.DisplayPromptAsync("Mensagem!", "", "OK", "Cancelar", "Digite sua mensagem!");
            try
            {
                if (!string.IsNullOrWhiteSpace(_msg))
                {
                    var _mensagem = new Mensagem
                    {
                        UsuarioID = UsuarioLogado.Codigo,
                        Msg = _msg,
                        MensagemGuid = Guid.NewGuid()
                    };
                    MensagemRepositorio.Add(_mensagem);

                    CarregarMensagens();
                }
            }
            catch (Exception)
            {

                await PageDialogService.DisplayAlertAsync("Error!", "Ocorreu um erro ao Adicionar essa menagem!", "OK");
            }

        }

        private void CarregarMensagens()
        {
            var _msg = MensagemRepositorio.GetByIdFather(UsuarioLogado.Codigo);
            if (_msg != null)
            {
                //await Connect();
                mensagens = new ObservableCollection<Mensagem>(_msg);
                //await hubConnection.InvokeAsync("SendMessage", UsuarioLogado.Codigo, mensagens);
            }
        }

        private async Task EnviarMensagemApi()
        {
            if (await MsgErrInterntOK("", "Sem Conexão com Internet!"))
                return;
            try
            {

                var errs = new List<string>();
                using (var Dialog = UserDialogs.Instance.Loading("Enviando...", null, null, true, MaskType.Black))
                {
                    //await Connect();
                    var _msgs = MensagemRepositorio.GetByIdFather(UsuarioLogado.Codigo);
                    foreach (var msg in _msgs)
                    {
                        var request = await MensagemService.PostAsync(msg);

                        if (request.Data.Equals("Mensagem Cadastrada"))
                            //await hubConnection.InvokeAsync("SendMessage", UsuarioLogado.Codigo, msg);


                            if (!request.IsSuccess)
                                errs.Add(request.Error);


                    }
                    if (errs.Count > 0)
                        await PageDialogService.DisplayAlertAsync("", $"{errs[0]}", "OK");
                    //await hubConnection.InvokeAsync("EnviarMenagem", UsuarioLogado.Codigo, _msgs);

                }
            }
            catch (Exception)
            {

                await PageDialogService.DisplayAlertAsync("Error!", "Ocorreu um erro ao Enviar essa menagem!", "OK");
            }
        }
        private async Task PegarMsgs(int codigo)
        {
            if (mensagens.Count == 0)
            {
                var result = await MensagemService.GetAllMensagensById(codigo);
                if (result.IsSuccess)
                {
                    if (result.Data.Count > 0)
                    {
                        foreach (var msg in result.Data)
                        {
                            var _msg = MensagemRepositorio.GetById(msg.Codigo);
                            if (_msg == null)
                                MensagemRepositorio.Add(msg);
                        }

                         CarregarMensagens();

                    }
                }
            }
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("_UsuarioKey"))
            {
                UsuarioLogado = parameters.GetValue<Usuario>("_UsuarioKey");
                //await CarregarMensagens();
                await PegarMsgs(UsuarioLogado.Codigo);
                UserDialogs.Instance.HideLoading();

            }
        }
        //public async override void Destroy()
        //{
        //    //await Disconnect();
        //}
    }
}
