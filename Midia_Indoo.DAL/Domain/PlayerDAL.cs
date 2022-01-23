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
    public class PlayerDAL
    {
        public PlayerDAL()
        {

        }


        public List<Player> GetAll()
        {
            var sql = "SELECT * FROM Player";
            using (var connection = new SqlConnection(BaseDAL.dbConn))
            {
                return connection.Query<Player>(sql).ToList();
            }
        }

        public List<Player> GetAllByCodigo(int id)
        {
            var sql = $"SELECT * FROM Player WHERE UsuarioID = {id}";
            using (var connection = new SqlConnection(BaseDAL.dbConn))
            {
                return connection.Query<Player>(sql).ToList();
            }
        }

        public string RecordPlayer(Player player)
        {
            var _newPlayer = new Player
            {
                StatusEnumID = 1,
                UsuarioID = player.UsuarioID
            };
            try
            {
                BaseDAL._context = new DB.DomainCtx();
                BaseDAL._context.Set<Player>().Add(_newPlayer);


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
