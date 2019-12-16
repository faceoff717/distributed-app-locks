namespace System.Distribution.Locks
{
    public enum LockResult
    {
        Acquired = 0,
        AcquiredAfterIncompatibleLocksRemoved = 1,
        AcquisitionTimeout = -1,
        AcquisitionCanceled = -2,
        Deadlocked = -3,
        Error = -999,
        Waiting = 1000,
    }
}