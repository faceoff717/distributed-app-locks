using Distributed.Locks.Sql;

namespace SampleLockApplication
{
    public class SampleLockMutex : SqlDistributedMutex
    {
        public SampleLockMutex() : base(ConnectionFactory.CreateConnection, nameof(SampleLockMutex))
        {
        }
    }
}