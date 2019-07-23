using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;

namespace Cinema.Agent
{
    internal class CacheWarmingUpThread
    {
        private readonly ConcurrentQueue<string> _urlQueue;

        public CacheWarmingUpThread(ConcurrentQueue<string> urlQueue)
        {
            _urlQueue = urlQueue;
        }

        public void Run()
        {
            string url;
            while (_urlQueue.TryDequeue(out url))
            {
                ProcessItem(url);
            }
        }

        private void ProcessItem(string url)
        {
            var stopWatch = new Stopwatch();
            try
            {
                stopWatch.Start();
                new WebClient().DownloadData(new Uri(Constants.Domain + url));// выполнение запроса
                
            }
            finally
            {
                stopWatch.Stop();
            }
        }
    }
}