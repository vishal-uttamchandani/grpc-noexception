using Grpc.Core;
using Grpcstreamserver;
using System;

namespace gRPCStreamServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // create service
            var service = new TestServiceImpl();

            // get port and host info
            var host = "localhost";
            var port = 50051;
            var grpcServer = new Server
            {
                Services = { TestService.BindService(service) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };
            grpcServer.Start();

            Console.WriteLine("gRPC Stream Server Started. Press any key to quit");
            Console.ReadLine();
            grpcServer.ShutdownTask.Wait();
        }
    }
}
