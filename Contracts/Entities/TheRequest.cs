namespace Contracts.Entities
{
    using System;
    using System.Collections.Generic;

    public class TheRequest
    {
        public TheRequest()
        {
            this.Id = Guid.NewGuid();
            this.RequestedDate = DateTime.UtcNow;
            this.CustomerCountries = new List<string>();
            this.TargetProducts = new List<KeyValuePair<string, string>>();
            this.TheTasks = new List<TheTask>();
        }

        public Guid Id { get; set; }

        public string Tenant { get; set; }

        public List<string> CustomerCountries { get; set; }

        public List<KeyValuePair<string, string>> TargetProducts { get; set; }

        public string RequestType { get; set; }

        public Guid RequestedBy { get; set; }

        public DateTime RequestedDate { get; set; }

        public List<TheTask> TheTasks { get; set; }
    }
}