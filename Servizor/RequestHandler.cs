namespace Servizor
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using Contracts.Commands;
    using MassTransit;
    using MassTransit.Courier;

    public class RequestHandler
    {
        public async Task ExecuteRequest(ConsumeContext<ProcessRequest> context)
        {
            try
            {
                // podemos criar o build dependendo do tipo de context

                var builder = new RoutingSlipBuilder(NewId.NextGuid());

                builder.AddActivity(ConfigurationManager.AppSettings["CreateTaskActivityName"], new Uri(ConfigurationManager.AppSettings["CreateTaskExecuteAddress"]));
                builder.AddActivity(ConfigurationManager.AppSettings["IdentifyProductsActivityName"], new Uri(ConfigurationManager.AppSettings["IdentifyProductsExecuteAddress"]));
                builder.AddActivity(ConfigurationManager.AppSettings["SendToExternalActivityName"], new Uri(ConfigurationManager.AppSettings["SendToExternalExecuteAddress"]));

                builder.SetVariables(new
                {
                    Id = context.Message.Id,
                    Tenant = context.Message.Tenant
                });

                var routingSlip = builder.Build();

                await context.Execute(routingSlip);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}