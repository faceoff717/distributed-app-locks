using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Distribution.Locks.Tests
{
    public class DatabaseFixture
    {
        public static SqlConnection CreateConnection()
        {
            SqlConnection connection =
                new SqlConnection(
                    "Initial Catalog=TestDatabase; Data Source=dbexample; user id=user; password=password;");
            return connection;
        }
    }
}
