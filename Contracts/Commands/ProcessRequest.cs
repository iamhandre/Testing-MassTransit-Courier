namespace Contracts.Commands
{
    using System;
    using System.Collections.Generic;

    public interface ProcessRequest
    {
        Guid Id { get; }

        string Tenant { get; set; }

        List<string> CustomerCountries { get; set; }

        Dictionary<string, string> TargetProducts { get; set; }

        string RequestType { get; set; }

        Guid RequestedBy { get; set; }

        DateTime RequestedDate { get; }
    }
}