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

## Key features

This toolset provides a convenient and reliable way to synchronize your applications located on different hosts. It combines C#'s lock and Mutex syntax usage advantages. The lock itself imlements Dispose pattern to release locked resources so you don't need to call methdos manualy, just put you'r critical lines of code inside using block and and there you have it. 

## Prerequisites

## Usage

```csharp
  using (var @lock = _mutex.WaitOne(10000))
  {
      Console.WriteLine(@lock.LockResult != LockResult.AcquisitionTimeout ?
              $"Job {job} acquired lock" : 
              $"Job {job} get resource by timeout");

      Thread.Sleep(3000);
      Console.WriteLine($"Job {job} releases resource");
  }
```

## Mutex based on Microsoft SQL Server implementation

### Prerequisities

### Creation of a mutex

## Best practices

