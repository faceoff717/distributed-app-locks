using System;
using System.Data.SqlClient;

namespace SampleLockApplication
{
    public static class ConnectionFactory
    {
        // TODO: you should to implement connection factory by yourself.
        // You should have any active MS SQL Server 2008 or newer
        public static SqlConnection CreateConnection()
        {
            throw new NotImplementedException();

            //var connectionString = "";
            //return new SqlConnection("");
        }
    }
}