using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EventAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] keys = { "Error", "Exception", "Fail", "Abort" };
            Dictionary<string, int> messages = new Dictionary<string, int>();
            EventLog eventLog = new EventLog("System");
            int count = eventLog.Entries.Count;
            EventLogEntry[] entries = new EventLogEntry[count];
            eventLog.Entries.CopyTo(entries, 0);
            Parallel.ForEach(entries, (item) =>
            {
                Parallel.ForEach(keys, (key) =>
                 {
                     if (item.Message.Contains(key))
                     {
                         if (!messages.ContainsKey(item.Message))
                         {
                             messages.Add(item.Message, 1);
                         }
                         else
                             ++messages[item.Message];
                     }
                 });

            });
            Console.ReadLine();
        }
    }
}
