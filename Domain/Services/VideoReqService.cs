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
    public class VideoReqService : BaseService, IVideoReqRepository
    {
        public async Task<ResponseService<List<VideoReq>>> GetAllVideoReqs()
        {
            var Uri = $"{BaseApiUrl}/VideoReq";
            ResponseService<List<VideoReq>> responseService = new ResponseService<List<VideoReq>>();
            HttpResponseMessage response = await _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var MsgJson = JsonConvert.DeserializeObject<List<VideoReq>>(Data);
                responseService.Data = MsgJson;
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

        public async Task<ResponseService<List<VideoReq>>> GetAllVideoReqsId(int PlayerId)
        {
            var Uri = $"{BaseApiUrl}/VideoReq/{PlayerId}";
            ResponseService<List<VideoReq>> responseService = new ResponseService<List<VideoReq>>();
            HttpResponseMessage response = await _Http.GetAsync(Uri);


            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {

                var Data = await response.Content.ReadAsStringAsync();
                var PersonJson = JsonConvert.DeserializeObject<List<VideoReq>>(Data);
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

        public async Task<ResponseService<string>> PostAsync(VideoReq playeReq)
        {
            var json = JsonConvert.SerializeObject(playeReq);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            HttpResponseMessage response = await _Http.PostAsync($"{BaseApiUrl}/VideoReq", stringContent);
            ResponseService<string> responseService = new ResponseService<string>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;


            if (response.IsSuccessStatusCode)
            {
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
    }
}
