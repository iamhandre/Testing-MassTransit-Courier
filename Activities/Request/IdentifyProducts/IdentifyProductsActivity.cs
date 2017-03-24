namespace Activities.Request.IdentifyProducts
{
    using System.Threading.Tasks;
    using MassTransit.Courier;

    public class IdentifyProductsActivity : Activity<IdentifyProductsArguments, IdentifyProductsLog>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<IdentifyProductsArguments> context)
        {
            var number = context.Arguments.NumberOfTasks;
            int tenant = 1;
            if (!int.TryParse(context.Arguments.Tenant, out tenant))
            {
                return context.Faulted(new System.Exception("Faulted!?!?"));
            }

            return context.Completed();
        }

        public async Task<CompensationResult> Compensate(CompensateContext<IdentifyProductsLog> context)
        {
            return context.Compensated();
        }
    }
}