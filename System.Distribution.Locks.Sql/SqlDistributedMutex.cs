using System.Data.SqlClient;
using System.Threading;

namespace System.Distribution.Locks.Sql
{
    public class SqlDistributedMutex : IDistributedMutex
    {
        private readonly Func<SqlConnection> _createConnection;
        private readonly string _mutexName;

        /// <summary>
        /// Creating distributed mutex instance
        /// </summary>
        /// <param name="createConnection">Connection factory</param>
        /// <param name="mutexName">Unique resource name</param>
        public SqlDistributedMutex(Func<SqlConnection> createConnection, string mutexName)
        {
            _createConnection = createConnection;
            _mutexName = mutexName;
        }

        /// <summary>
        /// Trying to obtain mutex until resource will be available or timeout expired
        /// </summary>
        /// <param name="timeout">Timeout to obtaining resource lock</param>
        /// <returns>State of the resource lock result</returns>
        public ILockState WaitOne(int timeout)
        {
            return CreateLocker(timeout).GetLock();
        }

        internal virtual Locker CreateLocker(int timeout)
        {
            return new Locker(_createConnection, _mutexName, timeout);
        }

        internal class Locker : ILockState
        {
            private readonly Func<SqlConnection> _createConnection;
            protected internal SqlCommand Command;

            private const string SqlCommandText = @"                    
                DECLARE @result int;
                DECLARE @applock_test int = 0;
                
                SELECT @applock_test = APPLOCK_TEST('public', @ResourceName, 'Exclusive', 'Transaction');

                IF (@applock_test = 0)
	                SELECT 1000;
                ELSE
                BEGIN                  
                    exec @result = sp_getapplock 
                        @Resource=@ResourceName, 
                        @LockMode='Exclusive', 
                        @LockOwner='Transaction', 
                        @LockTimeout = @LockTimeout
                    SELECT @result
                END";

            internal Locker(Func<SqlConnection> createConnection, string mutexName, int timeout)
            {
                LockResult = LockResult.Error;
                _createConnection = createConnection;
                MutexName = mutexName;
                Timeout = timeout;
            }

            public LockResult LockResult { get; private set; }

            internal ILockState GetLock()
            {
                DateTime dt = DateTime.Now;
                Command = BeginTransactionFor(CreateCommandWith(_createConnection()));
                
                while (LockResult != LockResult.Acquired)
                {
                    if (MillisecondsPassedFrom(dt) > Timeout)
                    {
                        LockResult = LockResult.AcquisitionTimeout;
                        break;
                    }
                    
                    LockResource();
                    
                    Thread.Sleep(100);
                }

                return this;
            }

            private double MillisecondsPassedFrom(DateTime dt)
            {
                return (DateTime.Now - dt).TotalMilliseconds;
            }

            protected internal virtual void LockResource()
            {
                LockResult = (LockResult) Command.ExecuteScalar();
            }

            private SqlCommand CreateCommandWith(SqlConnection connection)
            {
                var command = new SqlCommand(SqlCommandText, connection);

                command.Parameters
                    .AddWithValue("@ResourceName", MutexName);
                command.Parameters
                    .AddWithValue("@LockTimeout", 100);

                return command;
            }

            protected internal virtual SqlCommand BeginTransactionFor(SqlCommand command)
            {
                command.Connection.Open();
                command.Transaction = command.Connection.BeginTransaction();

                return command;
            }

            internal bool Disposed { get; private set; } = false;

            internal string MutexName { get; private set; }

            internal int Timeout { get; }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (Disposed)
                    return;

                if (disposing)
                {
                    DisposeMembers();
                }

                Disposed = true;
            }

            protected internal virtual void DisposeMembers()
            {
                Command.Transaction.Commit();
                Command.Connection.Close();
                Command.Connection.Dispose();
                Command.Dispose();
            }
        }
    }
}