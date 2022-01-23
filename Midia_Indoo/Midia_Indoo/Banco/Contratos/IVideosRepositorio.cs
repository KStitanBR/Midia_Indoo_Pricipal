using Domain.Models;
using Midia_Indoo.Banco.Contratos;
using System.Collections.Generic;

namespace Midia_Indoo.Banco.Contratos
{
    public interface IVideosRepositorio : IBaseRepositorio<Videos>
    {
        List<Videos> GetByIdFather(int codigo);

    }
}
