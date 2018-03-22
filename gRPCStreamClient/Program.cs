using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Logging;
using Grpc.Core.Utils;
using Grpcstreamserver;

namespace gRPCStreamClient
{
    class Program
    {
        static void Main()
        {
            GrpcEnvironment.SetLogger(new ConsoleLogger());

            // create channel
            var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

            // create client
            var client = new TestService.TestServiceClient(channel);

            DoStreamAsync(client).Wait();

            channel.ShutdownAsync().Wait();
        }

        private static async Task DoStreamAsync(TestService.TestServiceClient client)
        {
            var options = new CallOptions()
                .WithDeadline(DateTime.UtcNow.AddSeconds(5));

            var call = client.GetMessages(new MessageRequest(), options);

            try
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var message = call.ResponseStream.Current;
                    
                    // simulate work delay
                    await Task.Delay(1000);

                    Console.WriteLine($"Message: {message.MessageNumber}");
                }

                var status = call.GetStatus();
                Console.WriteLine(status.ToString());
                Console.WriteLine("Completed streaming");
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                call.Dispose();
            }
        }
    }
}
