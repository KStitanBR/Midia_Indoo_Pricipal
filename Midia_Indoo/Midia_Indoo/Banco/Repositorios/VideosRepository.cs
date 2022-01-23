using Domain.Models;
using Midia_Indoo.Banco.Contratos;
using Midia_Indoo.Banco.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Midia_Indoo.Banco.Repositorios
{
    public class VideosRepository : BaseRepositorio<Videos>, IVideosRepositorio
    {
        public VideosRepository()
        {
        }

        public List<Videos> GetByIdFather(int codigo)
        {
            var result = DbContext.Videos.ToList()
                                                  .Where(u => u.PlayerID.Equals(codigo)).ToList();
            return result;
        }
    }
}
