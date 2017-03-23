namespace Activities.Request.IdentifyProducts
{
    using System;

    public interface IdentifyProductsArguments
    {
        string Tenant { get; set; }

        Guid Id { get; set; }

        int NumberOfTasks { get; set; }
    }
}