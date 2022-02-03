using System;

namespace Distributed.Locks
{
    public interface IDistributedMutex
    {
        ILockState WaitOne(int timeout);
        ILockState WaitOne(TimeSpan timeout);
    }
}