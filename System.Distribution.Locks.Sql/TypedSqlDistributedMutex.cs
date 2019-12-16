using System;
using System.Data.SqlClient;

namespace Distributed.Locks.Sql
{
    public class SqlDistributedMutex<T> : SqlDistributedMutex, IDistributedMutex<T>
    {
        public SqlDistributedMutex(Func<SqlConnection> createConnection) : base(createConnection, typeof(T).Name)
        {
        }
    }
}