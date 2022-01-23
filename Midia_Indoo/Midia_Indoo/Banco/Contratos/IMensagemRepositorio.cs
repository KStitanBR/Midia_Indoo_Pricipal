using Domain.Models;
using System.Collections.Generic;

namespace Midia_Indoo.Banco.Contratos
{
    public interface IMensagemRepositorio : IBaseRepositorio<Mensagem>
    {
        List<Mensagem> GetByIdFather(int codigo);
    }
}
