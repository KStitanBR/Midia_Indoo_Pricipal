using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.Repository
{
    public interface IPlayeReqRepository
    {
        Task<ResponseService<List<PlayeReq>>> GetAllPlayerReqs();
        Task<ResponseService<UltimoAcessoPlayer>> GetAllPlayeReqsId(int PlayerId);
        Task<ResponseService<PlayeReq>> PostAsync(PlayeReq playeReq);
    }
}
