using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.DAL.Domain
{
   public class MensagemDAL
    {
        public MensagemDAL()
        {

        }
        public List<Mensagem> GetAll()
        {
            var sql = "SELECT * FROM Mensagem";
            using (var connection = new SqlConnection(BaseDAL.dbConn))
            {
                return connection.Query<Mensagem>(sql).ToList();
            }
        }

        public List<Mensagem> GetAllByCodigo(int id)
        {
            var sql = $"SELECT * FROM Mensagem WHERE UsuarioID = {id}";
            using (var connection = new SqlConnection(BaseDAL.dbConn))
            {
                return connection.Query<Mensagem>(sql).ToList();
            }
        }

        public string RecordPlayer(Mensagem msg)
        {
            var _newMsg = new Mensagem
            {
                UsuarioID = msg.UsuarioID,
                Msg = msg.Msg


            };
            try
            {
                BaseDAL._context = new DB.DomainCtx();
                BaseDAL._context.Set<Mensagem>().Add(_newMsg);


                BaseDAL._context.SaveChanges();

                return "OK";
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }
    }
}
