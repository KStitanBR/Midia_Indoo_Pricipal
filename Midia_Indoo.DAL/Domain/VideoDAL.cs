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
    public class VideoDAL
    {
        public VideoDAL()
        {

        }

        public List<Videos> GetAll()
        {
            var sql = "SELECT * FROM Player";
            using (var connection = new SqlConnection(BaseDAL.dbConn))
            {
                return connection.Query<Videos>(sql).ToList();
            }
        }

        public List<Videos> GetAllByCodigo(int id)
        {
            var sql = $"SELECT * FROM Player WHERE UsuarioID = {id}";
            using (var connection = new SqlConnection(BaseDAL.dbConn))
            {
                return connection.Query<Videos>(sql).ToList();

            }
        }
        public string RecordVideo(Videos videos)
        {
            var _newvideos = new Videos
            {
               Date = videos.Date,
               Nome = videos.Nome,
               PlayerID = videos.PlayerID,
               UriBase64 = videos.UriBase64
            };
            try
            {
                BaseDAL._context = new DB.DomainCtx();
                BaseDAL._context.Set<Videos>().Add(_newvideos);


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
