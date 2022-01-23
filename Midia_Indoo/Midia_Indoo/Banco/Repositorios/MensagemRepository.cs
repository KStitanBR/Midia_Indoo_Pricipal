using Domain.Models;
using Midia_Indoo.Banco.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Midia_Indoo.Banco.Repositorios
{
    public class MensagemRepository : BaseRepositorio<Mensagem>, IMensagemRepositorio
    {
        public MensagemRepository() 
        {
        }
        public List<Mensagem> GetByIdFather(int codigo)
        {
            var result = DbContext.Mensagens.ToList()
                                                  .Where(u => u.UsuarioID.Equals(codigo)).ToList();
            return result;
        }
    }
}
