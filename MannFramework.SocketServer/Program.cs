using System;
using System.Diagnostics;
using System.Threading;

namespace MannFramework.SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer server = new SocketServer();
            server.Start();
        }
    }
}
