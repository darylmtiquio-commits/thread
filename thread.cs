using System;
using System.Threading;

namespace ThreadDemo
{
    class Program
    {
        // Delegate to represent the methods executed by threads
        public delegate void ThreadWork();

        static void Main(string[] args)
        {
            // ---- Explanation ----
            // A process is an executing instance of a program. 
            // Each process has its own memory space.
            // A thread is the smallest unit of execution within a process. 
            // Multiple threads can run concurrently within the same process.

            // ---- Steps in creating a thread ----
            // 1. Define the method that the thread will run.
            // 2. Create a ThreadStart or ParameterizedThreadStart delegate that points to that method.
            // 3. Create a Thread object and pass the delegate.
            // 4. Call Start() on the Thread object to begin execution.

            // Create delegates
            ThreadWork countdownDelegate = new ThreadWork(CountDown);
            ThreadWork countupDelegate = new ThreadWork(CountUp);

            // Create threads
            Thread countdownThread = new Thread(countdownDelegate);
            Thread countupThread = new Thread(countupDelegate);

            // Demonstrating thread properties
            countdownThread.Name = "Countdown Thread";
            countupThread.Name = "Countup Thread";

            Console.WriteLine($"[Main] Starting {countdownThread.Name} (ID: {countdownThread.ManagedThreadId})");
            Console.WriteLine($"[Main] Starting {countupThread.Name} (ID: {countupThread.ManagedThreadId})");

            // Start threads
            countdownThread.Start();
            countupThread.Start();

            // Monitor main and worker threads until they finish
            Thread current = Thread.CurrentThread;
            current.Name = "Main Thread";

            while (countdownThread.IsAlive || countupThread.IsAlive)
            {
                Console.WriteLine($"[{current.Name}] Monitoring...");
                Console.WriteLine($"   -> ID: {current.ManagedThreadId}, State: {current.ThreadState}, IsAlive: {current.IsAlive}");
                Console.WriteLine($"   -> {countdownThread.Name}: State={countdownThread.ThreadState}, IsAlive={countdownThread.IsAlive}");
                Console.WriteLine($"   -> {countupThread.Name}: State={countupThread.ThreadState}, IsAlive={countupThread.IsAlive}");
                Thread.Sleep(700); // main thread pauses briefly while monitoring
            }

            // Ensure all threads are finished before completing
            countdownThread.Join();
            countupThread.Join();

            // Main thread output
            Console.WriteLine($"[{current.Name}] has been completed.");
        }

        // Countdown method
        static void CountDown()
        {
            Thread current = Thread.CurrentThread;
            for (int i = 5; i >= 0; i--)
            {
                Console.WriteLine($"[{current.Name}] Countdown: {i}");
                Console.WriteLine($"   -> ID: {current.ManagedThreadId}, State: {current.ThreadState}, IsAlive: {current.IsAlive}");
                Thread.Sleep(500); // Sleep demonstrates pausing execution
            }
            Console.WriteLine($"[{current.Name}] Finished execution. State: {current.ThreadState}, IsAlive: {current.IsAlive}");
        }

        // Countup method
        static void CountUp()
        {
            Thread current = Thread.CurrentThread;
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine($"[{current.Name}] Countup: {i}");
                Console.WriteLine($"   -> ID: {current.ManagedThreadId}, State: {current.ThreadState}, IsAlive: {current.IsAlive}");
                Thread.Sleep(500);
            }
            Console.WriteLine($"[{current.Name}] Finished execution. State: {current.ThreadState}, IsAlive: {current.IsAlive}");
        }
    }
}

/* --------------------------
   THREAD STATES
   --------------------------
   - Unstarted: Thread created but not started.
   - Running: Thread is executing.
   - WaitSleepJoin: Thread is paused (Sleep/Join).
   - Stopped: Thread has finished execution.
   
   --------------------------
   THREAD METHODS
   --------------------------
   - Start(): Starts the thread execution.
   - Sleep(ms): Suspends the thread for a period.
   - Join(): Blocks calling thread until the target thread terminates.
   - Abort() [deprecated]: Stops the thread forcefully (not safe).
   
   --------------------------
   THREAD PROPERTIES
   --------------------------
   - Name: Identifies the thread.
   - IsAlive: Checks if the thread is still running.
   - ThreadState: Current state of the thread.
   - ManagedThreadId: Unique ID for each thread.

   --------------------------
   THREAD LIFECYCLE DIAGRAM
   --------------------------
   
         +-------------+
         |  Unstarted  |
         +-------------+
                |
                v  Start()
         +-------------+
         |   Running   |<-------------------+
         +-------------+                    |
            |       |                       |
    Sleep() |       | Blocked (Join/Wait)   |
            v       v                       |
      +----------+  +------------------+    |
      | Sleeping |  | WaitSleepJoin    |----+
      +----------+  +------------------+
                |
                v
         +-------------+
         |   Stopped   |
         +-------------+

   Notes:
   - A thread starts in the Unstarted state.
   - After Start(), it becomes Running.
   - Running threads can be paused (Sleep), blocked (Join/Wait), or finish (Stopped).
   - Once stopped, a thread cannot be restarted.
*/
