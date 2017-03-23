namespace Servizor
{
    using System.Threading.Tasks;
    using Contracts.Commands;
    using MassTransit;

    internal class RequestConsumer : IConsumer<ProcessRequest>
    {
        private RequestHandler handler;

        public RequestConsumer(RequestHandler handler)
        {
            this.handler = handler;
        }

        public async Task Consume(ConsumeContext<ProcessRequest> context)
        {
            await this.handler.ExecuteRequest(context);
        }
    }
}