using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.Repository
{
   public interface IMensagemRepository
    {
        Task<ResponseService<List<Mensagem>>> GetAllMensagens();
        Task<ResponseService<List<Mensagem>>> GetAllMensagensById(int usuarioId);
        Task<ResponseService<string>> PostAsync(Mensagem msg);
        Task<ResponseService<Mensagem>> PostDeleteAsync(Mensagem msg);

    }
}
