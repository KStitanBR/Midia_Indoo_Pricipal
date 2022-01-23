using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.Repository
{
    public interface IUsuarioRepository
    {
        Task<ResponseService<Usuario>> GetUserAsync(string email, string senha);
        Task<ResponseService<List<Usuario>>> GetAllAsync();
        Task<ResponseService<Usuario>> PostAsync(Usuario usuario);
    }
}
