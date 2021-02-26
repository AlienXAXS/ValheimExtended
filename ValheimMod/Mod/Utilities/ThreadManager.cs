using System;
using System.Collections.Generic;
using System.Threading;
using Mod.Utilities;

namespace Util
{

    public class ThreadMemory
    {
        private Thread Thread { get; set; }
        private ThreadStart Action { get; set; }

        public ThreadMemory(ThreadStart action)
        {
            this.Action = action;
            Thread = new Thread(action);
        }

        public int GetThreadId()
        {
            return Thread.ManagedThreadId;
        }

        public string GetThreadActionMethodName()
        {
            return Action.Method.Name;
        }

        public void Kill()
        {
            Thread.Abort();
        }

        public void Start()
        {
            Thread.Start();
        }
    }

    public class ThreadManager : IDisposable
    {
        public static ThreadManager Instance = _instance ?? (_instance = new ThreadManager());
        private static readonly ThreadManager _instance;

        private readonly List<ThreadMemory> threads = new List<ThreadMemory>();

        public bool GameShutdownInitiated = false;

        public ThreadMemory CreateNewThread(ThreadStart action)
        {
            try
            {
                Logger.Log($"[ThreadManager-CreateNewThread] New Thread requested for method {action.Method.Name}", "yellow");
                threads.Add(new ThreadMemory(action));
                return threads[threads.Count-1];
            }
            catch (Exception ex)
            {
                Logger.Log($"[ThreadManager-CreateNewThread] New Thread requested for method {action.Method.Name} caused an exception", "red", ex);
            }

            return null;
        }

        public void KillAllThreads()
        {
            foreach (var thread in threads)
            {
                Logger.Log($"[ThreadManager-KillAllThreads] Attempting to kill thread {thread.GetThreadId()} on {thread.GetThreadActionMethodName()}", "yellow");
                thread.Kill();
            }
        }

        public void Dispose()
        {
            KillAllThreads();
        }
    }
}
