namespace System.Distribution.Locks
{
    public interface IDistributedMutex
    {
        ILockState WaitOne(int i);
    }
}