using System;
using System.Data.SqlClient;
using Distributed.Locks.Sql;

namespace Distributed.Locks.Tests
{
    public class TestMutex : SqlDistributedMutex
    {
        private readonly Func<SqlConnection> _createConnection;
        private readonly string _mutexName;

        public TestMutex(Func<SqlConnection> createConnection, string mutexName) : base(createConnection, mutexName)
        {
            _createConnection = createConnection;
            _mutexName = mutexName;
        }

        internal override Locker CreateLocker(int timeout)
        {
            return new TestLocker(_createConnection, _mutexName, timeout);
        }

        internal class TestLocker : Locker
        {
            private int _beginTransactionCalledTimes = 0;
            private bool _lockAcquired = false;

            internal TestLocker(Func<SqlConnection> createConnection, string mutexName, int timeout)
                : base(createConnection, mutexName, timeout)
            {
            }

            protected internal override void LockResource()
            {
                _lockAcquired = true;
            }

            protected internal override SqlCommand BeginTransactionFor(SqlCommand command)
            {
                _beginTransactionCalledTimes++;
                return command;
            }

            protected internal override void DisposeMembers()
            {
            }

            public bool BeginTransactionForCalled(int times)
            {
                return _beginTransactionCalledTimes == times;
            }

            public bool LockWasAcquired() => _lockAcquired;
        }
    }
}