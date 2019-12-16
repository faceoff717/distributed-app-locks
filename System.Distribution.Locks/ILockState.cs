using System;

namespace Distributed.Locks
{
    public interface ILockState : IDisposable
    {
        LockResult LockResult { get; }
    }
}