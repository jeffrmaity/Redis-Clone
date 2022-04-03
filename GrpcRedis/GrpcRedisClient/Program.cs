using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcRedisClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var channel = new Channel("127.0.0.1:30052", ChannelCredentials.Insecure);
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            Console.WriteLine("Welcome to Mini Redis Clone System 2022");

            bool endApp = false;
            while (!endApp)
            {
                Console.WriteLine("Kindly Enter Your Command:");
                string command = Console.ReadLine();
                if (command.ToUpper().Equals("EXIT"))
                {
                    endApp = true;
                    Environment.Exit(0);
                }
                else
                {
                    var reply = await client.ExecuteCommandAsync(new Command { Command_ = command });
                    Console.WriteLine(reply);
                }
            }
            Console.ReadKey();
        }
    }
}
