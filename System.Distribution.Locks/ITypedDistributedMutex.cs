namespace Distributed.Locks
{
    public interface IDistributedMutex<T>
    {
        ILockState WaitOne(int i);
    }
}