using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpcstreamserver;

namespace gRPCStreamServer
{
    public class TestServiceImpl : TestService.TestServiceBase
    {
        public override async Task GetMessages(
            MessageRequest request,
            IServerStreamWriter<TestMessage> responseStream, 
            ServerCallContext context)
        {
            for (var i = 1; i <= 20; i++)
            {
                Console.WriteLine($"Sent Message: {i}");
                await responseStream.WriteAsync(new TestMessage
                {
                    MessageNumber = i
                });
            }
        }
    }
}
