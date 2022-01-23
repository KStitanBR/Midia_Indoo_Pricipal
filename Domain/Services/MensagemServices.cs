using Domain.Models;
using Midia_Indoo.Repository;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Domain.Services
{
    public class MensagemServices : BaseService, IMensagemRepository
    {
        public async Task<ResponseService<List<Mensagem>>> GetAllMensagens()
        {
            var Uri = $"{BaseApiUrl}/Msg/All";


            ResponseService<List<Mensagem>> responseService = new ResponseService<List<Mensagem>>();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(key: Uri))
            {
                responseService.Data = Barrel.Current.Get<List<Mensagem>>(key: Uri);
                return responseService;
            }
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                responseService.StatusCode = 404;
                return responseService;
            }

            HttpResponseMessage response = await _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Mensagem>>(Data);
                responseService.Data = PersonJson;

            }
            else
            {
                var problemResponse = response.Content.ReadAsStringAsync();
                //var errors = JsonConvert.DeserializeObject<ResponseService<List<Routine>>>(problemResponse);


                //responseService.Errors = problemResponse;
            }

            //Barrel.Current.Add(key: Uri, data: responseService.Data, expireIn: TimeSpan.FromHours(60));
            return responseService;
        }

        public async Task<ResponseService<List<Mensagem>>> GetAllMensagensById(int usuarioId)
        {
            var Uri = $"{BaseApiUrl}/Mensagem/{usuarioId}";
            ResponseService<List<Mensagem>> responseService = new ResponseService<List<Mensagem>>();

            HttpResponseMessage response = _Http.GetAsync(Uri).Result;

            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Mensagem>>(Data);
                responseService.Data = PersonJson;

            }
            else
            {
                var problemResponse = response.Content.ReadAsStringAsync();
                //var errors = JsonConvert.DeserializeObject<ResponseService<List<Routine>>>(problemResponse);


                //responseService.Errors = problemResponse;
            }

            //Barrel.Current.Add(key: Uri, data: responseService.Data, expireIn: TimeSpan.FromMinutes(30));
            return responseService;
        }

        public async Task<ResponseService<string>> PostAsync(Mensagem msg)
        {
            var json = JsonConvert.SerializeObject(msg);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/Mensagem", stringContent);
            ResponseService<string> responseService = new ResponseService<string>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                var Data = await response.Content.ReadAsStringAsync();
                //var Json = JsonConvert.DeserializeObject<string>(Data);
                responseService.Data = Data;
            }
            else
            {
                var problemResponse = response.Content.ReadAsStringAsync();

                //ResponseService<Person> problemResponse = response.Content.ReadAsStringAsync();
                //responseService.Errors = problemResponse.Errors;
            }
            return responseService;

        }

        public async Task<ResponseService<Mensagem>> PostDeleteAsync(Mensagem msg)
        {
            var json = JsonConvert.SerializeObject(msg);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/Mensagem/Deletar", stringContent);
            ResponseService<Mensagem> responseService = new ResponseService<Mensagem>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                //var Data = await response.Content.ReadAsStringAsync();
                //var Json = JsonConvert.DeserializeObject<Mensagem>(Data);
                //responseService.Data = Json;
            }
            else
            {
                var problemResponse = response.Content.ReadAsStringAsync();

                //ResponseService<Person> problemResponse = response.Content.ReadAsStringAsync();
                responseService.Error = problemResponse.Result;
            }
            return responseService;

        }
    }
}
