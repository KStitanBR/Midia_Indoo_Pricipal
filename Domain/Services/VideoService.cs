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
    public class VideoService : BaseService, IVideosRepository
    {
        public async Task<ResponseService<List<Videos>>> GetAllVideos()
        {
            var Uri = $"{BaseApiUrl}/Videos/all";
            ResponseService<List<Videos>> responseService = new ResponseService<List<Videos>>();
            HttpResponseMessage response = await  _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Videos>>(Data);
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

        public async Task<ResponseService<List<Videos>>> GetAllVideosById(int playerId)
        {
            var Uri = $"{BaseApiUrl}/Video/{playerId}";
            ResponseService<List<Videos>> responseService = new ResponseService<List<Videos>>();

            //if (Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(key: Uri))
            //{
            //    responseService.Data = Barrel.Current.Get<List<Usuario>>(key: Uri);
            //    return responseService;
            //}

            //if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            //{
            //    responseService.StatusCode = 404;
            //    return responseService;
            //}

            HttpResponseMessage response = _Http.GetAsync(Uri).Result;


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Videos>>(Data);
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

        public async Task<ResponseService<string>> PostAsync(Videos videos)
        {

            var json = JsonConvert.SerializeObject(videos);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/Video", stringContent);
            ResponseService<string> responseService = new ResponseService<string>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                //var Data =
                //var Json = JsonConvert.DeserializeObject<Videos>(Data);
                responseService.Data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                var problemResponse = response.Content.ReadAsStringAsync();
                responseService.Error = problemResponse.ToString();
                //ResponseService<Person> problemResponse = response.Content.ReadAsStringAsync();
                //responseService.Errors = problemResponse.Errors;
            }
            return responseService;
        }

        //public async Task<ResponseService<Videos>> PostDeleteAsync(Videos videos)
        //{
        //    var json = JsonConvert.SerializeObject(videos);
        //    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
        //    HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/Video/Deletar", stringContent);
        //    ResponseService<Videos> responseService = new ResponseService<Videos>();
        //    responseService.IsSuccess = response.IsSuccessStatusCode;
        //    responseService.StatusCode = (int)response.StatusCode;


        //    if (response.IsSuccessStatusCode)
        //    {
        //        //var Data = await response.Content.ReadAsStringAsync();
        //        //var Json = JsonConvert.DeserializeObject<Videos>(Data);
        //        //responseService.Data = Json;
        //    }
        //    else
        //    {
        //        var problemResponse = response.Content.ReadAsStringAsync();

        //        //ResponseService<Person> problemResponse = response.Content.ReadAsStringAsync();
        //        //responseService.Errors = problemResponse.Errors;
        //    }
        //    return responseService;
        //}

        public async Task<ResponseService<string>> DeleteByGuidAsync(Guid guid)
        {
            var Uri = $"{BaseApiUrl}/Video/Deletar?guid={guid}";
            ResponseService<string> responseService = new ResponseService<string>();
            HttpResponseMessage response = await _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                //var Data = 
                //var PersonJson = JsonConvert.DeserializeObject<string>(Data);
                responseService.Data = await response.Content.ReadAsStringAsync(); 

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

        public async Task<ResponseService<bool>> SeachVideoAsync(Guid guid)
        {
            var Uri = $"{BaseApiUrl}/Video/Search?guid={guid}";
            ResponseService<bool > responseService = new ResponseService<bool>();
            HttpResponseMessage response = await _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<bool>(Data);
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

        public async Task<ResponseService<string>> PostDeleteAsync(Videos videos)
        {
            var json = JsonConvert.SerializeObject(videos);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/Video/Deletar", stringContent);
            ResponseService<string> responseService = new ResponseService<string>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
                //var Data = await response.Content.ReadAsStringAsync();
                //var Json = JsonConvert.DeserializeObject<Videos>(Data);
                responseService.Data = await response.Content.ReadAsStringAsync(); 
            }
            else
            {
                var problemResponse = response.Content.ReadAsStringAsync();

                //ResponseService<Person> problemResponse = response.Content.ReadAsStringAsync();
                //responseService.Errors = problemResponse.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<List<Videos>>> GetPartVideo(int codigo)
        {
            ResponseService<List<Videos>> responseService = new ResponseService<List<Videos>>();
            HttpResponseMessage response = await _Http.GetAsync($"{BaseApiUrl}/Video/GetPartVideo/{codigo}");


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<Videos>>(Data);
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
    }
}
