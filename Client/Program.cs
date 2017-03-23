namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Contracts.Entities;
    using MassTransit;
    using MassTransit.Util;

    public class Program
    {
        private static IBusControl _bus;

        private static void Main(string[] args)
        {
            try
            {
                _bus = CreateBus();
                TaskUtil.Await(() => _bus.StartAsync());
                var endpoint = TaskUtil.Await(() => _bus.GetSendEndpoint(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"] + ConfigurationManager.AppSettings["RabbitMQQueueRequest"])));
                endpoint.Send(CreateRequestCall());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IBusControl CreateBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["RabbitMQUsername"]);
                    h.Password(ConfigurationManager.AppSettings["RabbitMQPassword"]);
                });
            });
        }

        private static TheRequestCall CreateRequestCall()
        {
            return new TheRequestCall
            {
                Tenant = "1",
                RequestedBy = Guid.NewGuid(),
                RequestType = "fresh",
                CustomerCountries = new List<string> { "PT", "ES", "FR" },
                TargetProducts = new Dictionary<string, string>
                {
                    { "a", "d2b25ba2-1fde-45b4-be4d-0153a8091713" }
                }
            };
        }
    }
}