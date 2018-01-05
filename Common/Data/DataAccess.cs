using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puma.Prey.Common.Data
{
    public class DataAccess
    {
        public static bool HuntIsValid(string id)
        {
			SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["PumaPreyConnectionString"].ConnectionString);
			try
            {
                if (!Int32.TryParse(id, out int cID))
                { return false; }

                cn.Open();
                
                SqlCommand cmd = new SqlCommand("select count(*) from Hunt where ID = " + cID.ToString() + " and Closed = 0", cn);
                string result = cmd.ExecuteScalar().ToString();

				if (Int32.TryParse(result, out int count) && count > 0)
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
