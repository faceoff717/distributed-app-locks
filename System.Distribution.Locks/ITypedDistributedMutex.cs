namespace System.Distribution.Locks
{
    public interface IDistributedMutex<T>
    {
        ILockState WaitOne(int i);
    }
}