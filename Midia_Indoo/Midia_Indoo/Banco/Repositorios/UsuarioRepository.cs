using Domain.Models;
using Midia_Indoo.Banco.Contratos;
using Midia_Indoo.Banco.Repositorios;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Midia_Indoo.Banco.Repositorios
{
    public class UsuarioRepository : BaseRepositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepository()
        {
        }

        public Usuario GetByUser(string email, string senha)
        {
            var result =  DbContext.Usuarios.ToList()
                                                  .FirstOrDefault(u =>
                                                            (u.Email ?? "").ToUpper().Equals(email.ToUpper() ?? "")
                                                            && (u.Senha ?? "").ToUpper().Equals(senha.ToUpper() ?? ""));
            return result;
        }
    }
}
