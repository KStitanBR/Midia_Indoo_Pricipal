using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Midia_Indoo.Banco.Contratos
{
    public interface IPlayerRepositorio : IBaseRepositorio<Player>
    {
        List<Player> GetByIdFather(int codigo);
    }
}
