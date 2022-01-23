using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.Repository
{
    public interface IVideoReqRepository
    {
        Task<ResponseService<List<VideoReq>>> GetAllVideoReqs();
        Task<ResponseService<List<VideoReq>>> GetAllVideoReqsId(int PlayerId);
        Task<ResponseService<string>> PostAsync(VideoReq playeReq);
    }
}
