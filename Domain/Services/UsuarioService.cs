using Domain.Models;
using Midia_Indoo.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Domain.Services
{
    public class UsuarioService : BaseService, IUsuarioRepository
    {
        public async Task<ResponseService<List<Usuario>>> GetAllAsync()
        {
            var Uri = $"{BaseApiUrl}/Usuarios/all";
            ResponseService<List<Usuario>> responseService = new ResponseService<List<Usuario>>();
            HttpResponseMessage response =  await  _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Usuario>>(Data);
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

        public async Task<ResponseService<Usuario>> GetUserAsync(string email, string senha)
        {
            var Uri = $"{BaseApiUrl}/Usuario?email={email}&senha={senha}";
            ResponseService<Usuario> responseService = new ResponseService<Usuario>();
            var response = await _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<Usuario>(Data);
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


        public async Task<ResponseService<Usuario>> PostAsync(Usuario usuario)
        {
            var json = JsonConvert.SerializeObject(usuario);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/Usuario", stringContent);
            ResponseService<Usuario> responseService = new ResponseService<Usuario>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                var Data = await response.Content.ReadAsStringAsync();
                var Json = JsonConvert.DeserializeObject<Usuario>(Data);
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
    }
}
