namespace Activities.Request.CreateTask
{
    using System;

    public interface CreateTaskArguments
    {
        string Tenant { get; set; }

        Guid Id { get; set; }
    }
}