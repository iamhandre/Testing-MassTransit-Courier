namespace Contracts.Entities
{
    using System;
    using System.Collections.Generic;

    public class TheTask
    {
        public TheTask()
        {
            this.Id = Guid.NewGuid();
            this.Products = new List<int>();
        }

        public Guid Id { get; set; }

        public Guid? ExternalId { get; set; }

        public string CustomerCountry { get; set; }

        public Guid Store { get; set; }

        public List<int> Products { get; set; }
    }
}