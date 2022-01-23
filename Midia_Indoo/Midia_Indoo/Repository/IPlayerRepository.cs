using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.Repository
{
    public interface IPlayerRepository
    {
        Task<ResponseService<List<Player>>> GetAllPlayers();
        Task<ResponseService<List<Player>>> GetAllPlayersId(int usuarioID);
        Task<ResponseService<Player>> PostAsync(Player player);
        Task<ResponseService<Player>> PostDeleteAsync(Player player);
    }
}
