# Distributed application lock toolset
Synchronization toolset for distributed applications provides utilities for cross-server mutex implementation.

## Contents

* [Contents](#contents)
* [Key Features](#key-features)
* [Prerequisites](#prerequisites)
* [Usage](#usage)
* [MSSQL-based mutex](#mutex-based-on-microsoft-sql-server-implementation)
  * [Prerequisities](#prerequisities)
  * [Creation of a mutex](#creation-of-a-mutex)
* [Licence](#licence)

## Key features

This toolset provides a convenient and reliable way to synchronize your applications located on different hosts. It combines C#'s lock and Mutex syntax usage advantages. The lock itself imlements Dispose pattern to release locked resources so you don't need to call methdos manualy, just put you'r critical lines of code inside using block and and there you have it. 

## Prerequisites

Library is using .NET Standard 2.0. It can be used with .NET Core 2.2 or .NET Framework 4.5.2 out of the box. 

## Usage

First of all, you need to create a unique-named mutex to protect your resourse(s) from parallel access. More details you can get in implementation details of the mutexes.

The usage of the mutex is pretty simple, all that you need is to create a *using* block

```csharp
  IDistributedMutex mutex = CreateSomeMutex();
  
  using (mutex.WaitOne(10000))
  {
      // Use resource you want to protect from paralell access
  }
```

At that sample, the mutex will obtain and lock your resource or wait 10 seconds, and after that will give control to your code back, if it was failed to obtain the lock. To handle that you can use *ILockState* result:

```csharp
  using (var @lock = mutex.WaitOne(10000))
  {
      Console.WriteLine(@lock.LockResult != LockResult.AcquisitionTimeout ?
              $"Job {job} acquired lock" : 
              $"Job {job} get resource by timeout");

      // Use resource you want to protect from paralell access
      Console.WriteLine($"Job {job} releases resource");
  }
```

Lock result can contain the next values: 
     * Acquired
     * AcquiredAfterIncompatibleLocksRemoved
     * AcquisitionTimeout
     * AcquisitionCanceled
     * Deadlocked
     * Error

## Mutex based on Microsoft SQL Server implementation

This implementation is using stored procedure sp_getapplock shipped with Microsoft SQL Server. More information about it you can find [here](https://docs.microsoft.com/ru-ru/sql/relational-databases/system-stored-procedures/sp-getapplock-transact-sql?view=sql-server-2017 "sp_getapplock (Transact-SQL)")

### Prerequisities 

### Creation of a mutex

## Best practices

## Licence
