using Distributed.Locks.Sql;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Distributed.Locks.Tests
{
    public class DistributedMutexShould
    {
        private const string ExpectedName = "MyMutex";
        private const int ExpectedTimeout = 10;

        private readonly SqlDistributedMutex _testSqlDistributedMutex;

        public DistributedMutexShould()
        {
            _testSqlDistributedMutex = new TestMutex(DatabaseFixture.CreateConnection, ExpectedName);
        }

        [Fact]
        public void Create_Lock_With_Name()
        {
            using (var lockResult = (SqlDistributedMutex.Locker) _testSqlDistributedMutex.WaitOne(ExpectedTimeout))
                lockResult.MutexName.Should().Be(ExpectedName);
        }

        [Fact]
        public void Set_Correct_Timeout()
        {
            using (var lockResult = (SqlDistributedMutex.Locker) _testSqlDistributedMutex.WaitOne(ExpectedTimeout))
                lockResult.Timeout.Should().Be(ExpectedTimeout);
        }

        [Fact]
        public void Execute_Transaction()
        {
            using (var lockResult = (TestMutex.TestLocker) _testSqlDistributedMutex.WaitOne(ExpectedTimeout))
                lockResult.BeginTransactionForCalled(1).Should().BeTrue();
        }

        [Fact]
        public void Have_Proper_Command_Params()
        {
            using (var lockResult = (TestMutex.TestLocker) _testSqlDistributedMutex.WaitOne(ExpectedTimeout))
            {
                using (new AssertionScope())
                {
                    lockResult.Command.Parameters["@ResourceName"].Value.Should().Be(ExpectedName);
                    lockResult.Command.Parameters["@LockTimeout"].Value.Should().Be(100);
                }
            }
        }

        [Fact]
        public void Acquire_Lock()
        {
            using (var lockResult = (TestMutex.TestLocker) _testSqlDistributedMutex.WaitOne(ExpectedTimeout))
                lockResult.LockWasAcquired().Should().BeTrue();
        }

        [Fact]
        public void Be_Disposed_After_Usage()
        {
            TestMutex.TestLocker mutex;
            using (var lockResult = (TestMutex.TestLocker) _testSqlDistributedMutex.WaitOne(ExpectedTimeout))
                mutex = lockResult;

            mutex.Disposed.Should().BeTrue();
        }
    }
}