namespace System.Distribution.Locks
{
    public interface ILockState : IDisposable
    {
        LockResult LockResult { get; }
    }
}
