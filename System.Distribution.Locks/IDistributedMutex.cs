namespace Distributed.Locks
{
    public interface IDistributedMutex
    {
        ILockState WaitOne(int i);
    }
}