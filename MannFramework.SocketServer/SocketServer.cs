using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MannFramework.SocketServer
{
    public class SocketServer
    {
        public virtual void Start()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = "server.js";
            start.FileName = "node.exe";
            start.WindowStyle = ProcessWindowStyle.Normal;
            start.CreateNoWindow = true;
            int exitCode;

            while (true)
            {
                using (Process proc = Process.Start(start))
                {
                    proc.WaitForExit();
                    exitCode = proc.ExitCode;
                    Console.WriteLine(exitCode);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
