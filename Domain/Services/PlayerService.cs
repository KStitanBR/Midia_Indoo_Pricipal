using Domain.Models;
using Midia_Indoo.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PlayerService : BaseService, IPlayerRepository
    {
        public async Task<ResponseService<List<Player>>> GetAllPlayers()
        {
            var Uri = $"{BaseApiUrl}/Player/all";
            ResponseService<List<Player>> responseService = new ResponseService<List<Player>>();
            HttpResponseMessage response = await _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Player>>(Data);
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

        public async Task<ResponseService<List<Player>>> GetAllPlayersId(int usuarioID)
        {
            var Uri = $"{BaseApiUrl}/Player/{usuarioID}";
            ResponseService<List<Player>> responseService = new ResponseService<List<Player>>();
            HttpResponseMessage response = _Http.GetAsync(Uri).Result;


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Player>>(Data);
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

        public async Task<ResponseService<Player>> PostAsync(Player player)
        {
            var json = JsonConvert.SerializeObject(player);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/Player", stringContent);
            ResponseService<Player> responseService = new ResponseService<Player>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                var Data = await response.Content.ReadAsStringAsync();
                var Json = JsonConvert.DeserializeObject<Player>(Data);
                responseService.Data = Json;
            }
            else
            {
                var problemResponse = response.Content.ReadAsStringAsync();

                //ResponseService<Person> problemResponse = response.Content.ReadAsStringAsync();
                //responseService.Errors = problemResponse.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Player>> PostDeleteAsync(Player player)
        {
            var json = JsonConvert.SerializeObject(player);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/Player/Deletar", stringContent);
            ResponseService<Player> responseService = new ResponseService<Player>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                //var Data = await response.Content.ReadAsStringAsync();
                //var Json = JsonConvert.DeserializeObject<Player>(Data);
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
