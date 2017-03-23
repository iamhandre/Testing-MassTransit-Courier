namespace Activities.Request.CreateTask
{
    using System.Threading.Tasks;
    using MassTransit.Courier;

    public class CreateTaskActivity : ExecuteActivity<CreateTaskArguments>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<CreateTaskArguments> context)
        {
            var arguments = context.Arguments;

            return context.CompletedWithVariables(new
            {
                NumberOfTasks = 10
            });
        }
    }
}