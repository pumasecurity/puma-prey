using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Puma.Prey.Rabbit.EF;

namespace Puma.Prey.Raccoon
{
    public class HuntUtil
    {
        public static bool HuntIsValid(string id)
        {
            Int32 hId;
            var context = new RabbitDBContext();
            var cn = new SqliteConnection(((DbContext)context).Database.GetDbConnection().ConnectionString);
            try
            {
                if (!Int32.TryParse(id, out hId))
                { return false; }

                cn.Open();

                var cmd = new SqliteCommand("select count(*) from Contests where ID = " + hId.ToString() + " and Closed = 0", cn);
                string result = cmd.ExecuteScalar().ToString();

                Int32 count;
                if (Int32.TryParse(result, out count) && count > 0)
                { return true; }

                return false;
            }
            finally
            {
                if (cn.State != System.Data.ConnectionState.Closed) { cn.Close(); }
            }
        }
    }
}