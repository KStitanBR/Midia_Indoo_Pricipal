using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Midia_Indoo.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.DAL.Domain
{
    public class UsuarioDAL
    {
        public UsuarioDAL()
        {
        }

        public List<Usuario> GetAll()
        {
            string sql = "Select * From Usuario";

            using (var connection = new SqlConnection(BaseDAL.dbConn))
            {
                return connection.Query<Usuario>(sql).ToList();
            }
        }
        public Usuario Get(string email, string senha)
        {
            var user = this.GetAll().FirstOrDefault(l => (l.Email ?? "").ToLower().Equals((email ?? "").ToLower()) &&
                                                      (l.Senha ?? "").ToLower().Equals((senha ?? "").ToUpper()));

            return user;
        }
    }
}
