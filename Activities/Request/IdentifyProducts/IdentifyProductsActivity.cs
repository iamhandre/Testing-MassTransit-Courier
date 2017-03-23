namespace Activities.Request.IdentifyProducts
{
    using System;
    using System.Threading.Tasks;
    using MassTransit.Courier;

    public class IdentifyProductsActivity : Activity<IdentifyProductsArguments, IdentifyProductsLog>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<IdentifyProductsArguments> context)
        {
            var number = context.Arguments.NumberOfTasks;

            return context.Completed();
        }

        public Task<CompensationResult> Compensate(CompensateContext<IdentifyProductsLog> context)
        {
            throw new NotImplementedException();
        }
    }
}