using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.Repository
{
   public interface IVideosRepository
    {
        Task<ResponseService<List<Videos>>> GetAllVideos();
        Task<ResponseService<List<Videos>>> GetAllVideosById(int playerId);
        Task<ResponseService<string>> PostAsync(Videos videos);
        //Task<ResponseService<Videos>> PostDeleteAsync(Videos videos);
        Task<ResponseService<List<Videos>>> GetPartVideo(int codigo);
        Task<ResponseService<string>> DeleteByGuidAsync(Guid guid);
        Task<ResponseService<string>> PostDeleteAsync(Videos videos);
        Task<ResponseService<bool>> SeachVideoAsync(Guid guid);


    }
}
