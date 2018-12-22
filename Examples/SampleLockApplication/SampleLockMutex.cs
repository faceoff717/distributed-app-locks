using System;
using System.Data.SqlClient;
using System.Distribution.Locks.Sql;


namespace SampleLockApplication
{
    public class SampleLockMutex : SqlDistributedMutex
    {
        public SampleLockMutex() : base(ConnectionFactory.CreateConnection, nameof(SampleLockMutex))
        {
            
        }        
    }

  
}
