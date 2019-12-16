using System.Data.SqlClient;

namespace Distributed.Locks.Tests
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
