using Midia_Indoo.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.DAL.Domain
{
    public static class BaseDAL
    {
        public static string dbConn = @"Data Source=.\SQLEXPRESS2014; Initial Catalog=MediaIndoor; User Id=sa; Password=12300456; MultipleActiveResultSets = true;";

        public static DomainCtx _context { get; set; }
    }
}
