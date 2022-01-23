using Domain.Models;
using Midia_Indoo.Banco.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Midia_Indoo.Banco.Repositorios
{
    public class PlayerRepository : BaseRepositorio<Player>, IPlayerRepositorio
    {
        public PlayerRepository()
        {
        }

        public List<Player> GetByIdFather(int codigo)
        {
            var result = DbContext.Players.ToList()
                                                  .Where(u => u.UsuarioID.Equals(codigo)).ToList();
            return result;
        }
    }
}
