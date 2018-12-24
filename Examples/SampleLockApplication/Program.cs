using System;
using System.Distribution.Locks;
using System.Threading;

namespace SampleLockApplication
{
    class Program
    {
        private static IDistributedMutex _mutex = new SampleLockMutex();

        static void Main()
        {
            Console.WriteLine("Sample of distributed application lock");

            Thread a = new Thread(() => MakeSomeJob("A"));
            Thread b = new Thread(() => MakeSomeJob("B"));
            Thread c = new Thread(() => MakeSomeJob("C"));
            Thread d = new Thread(() => MakeSomeJob("D"));
            Thread e = new Thread(() => MakeSomeJob("E"));

            a.Start();
            b.Start();
            c.Start();
            d.Start();
            e.Start();

            Console.ReadKey();
        }

        private static void MakeSomeJob(string job)
        {
            Console.WriteLine($"Job {job} is waiting for resource to be free");
            using (var @lock = _mutex.WaitOne(10000))
            {
                Console.WriteLine(@lock.LockResult != LockResult.AcquisitionTimeout
                    ? $"Job {job} acquired lock"
                    : $"Job {job} get resource by timeout");

                Thread.Sleep(3000);
                Console.WriteLine($"Job {job} releases resource");
            }
        }
    }
}