using System.Data.SqlClient;

namespace System.Distribution.Locks.Sql
{
    public class SqlDistributedMutex<T> : SqlDistributedMutex, IDistributedMutex<T>
    {
        public SqlDistributedMutex(Func<SqlConnection> createConnection) : base(createConnection, typeof(T).Name)
        {
        }
    }
}