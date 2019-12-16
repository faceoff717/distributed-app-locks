using Distributed.Locks.Sql;
using FluentAssertions;
using Xunit;

namespace Distributed.Locks.Tests
{
    public class TypedDistributedMutexShould
    {
        private SqlDistributedMutex _mutex;

        [Fact]
        public void To_Be_Created_With_Generic_Mutex_Name()
        {
            _mutex = new SqlDistributedMutex<ILocker>(DatabaseFixture.CreateConnection);
            _mutex.CreateLocker(0).MutexName.Should().Be("ILocker");
        }

        private interface ILocker { }
    }
}
