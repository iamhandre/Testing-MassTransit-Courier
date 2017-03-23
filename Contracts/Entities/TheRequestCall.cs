namespace Contracts.Entities
{
    using System;
    using System.Collections.Generic;
    using Commands;

    public class TheRequestCall : ProcessRequest
    {
        public TheRequestCall()
        {
            this.Id = Guid.NewGuid();
            this.RequestedDate = DateTime.UtcNow;
            this.CustomerCountries = new List<string>();
            this.TargetProducts = new Dictionary<string, string>();
        }

        public Guid Id { get; private set; }

        public string Tenant { get; set; }

        public List<string> CustomerCountries { get; set; }

        public Dictionary<string, string> TargetProducts { get; set; }

        public string RequestType { get; set; }

        public Guid RequestedBy { get; set; }

        public DateTime RequestedDate { get; private set; }
    }
}