using Domain.Models;
using Midia_Indoo.Banco.Contratos;
using System.Threading.Tasks;

namespace Midia_Indoo.Banco.Contratos
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        Usuario GetByUser(string email, string senha);
    }
}
