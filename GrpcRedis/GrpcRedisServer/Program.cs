using Grpc.Core;
using System;
using System.Collections.Generic;

namespace GrpcRedisServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int Port = 30052;

            var variables = new List<Variable>();

            Server server = new Server
            {
                Services = { Greeter.BindService(new GreeterService(variables)) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("GrpcRedis server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
