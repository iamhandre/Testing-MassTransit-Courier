namespace Servizor
{
    using System;
    using System.Configuration;
    using Activities.Request.CreateTask;
    using Activities.Request.IdentifyProducts;
    using MassTransit;
    using Topshelf;
    using Topshelf.Logging;

    public class RequesterService : ServiceControl
    {
        private IBusControl _busControl;

        private readonly LogWriter _log = HostLogger.Get<RequesterService>();

        public bool Start(HostControl hostControl)
        {
            try
            {
                _log.Info("Creating bus...");

                MassTransitServiceBus();
                InitializeServiceBus();

                _log.Info("Starting bus...");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }

        public void MassTransitServiceBus()
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(h =>
            {
                var host = h.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), hi =>
                {
                    hi.Username(ConfigurationManager.AppSettings["RabbitMQUsername"]);
                    hi.Password(ConfigurationManager.AppSettings["RabbitMQPassword"]);
                });

                // Endpoint do consumer
                h.ReceiveEndpoint(host, ConfigurationManager.AppSettings["RabbitMQQueueRequest"], e =>
                {
                    e.Consumer(() =>
                    {
                        var handler = new RequestHandler();
                        return new RequestConsumer(handler);
                    });
                });

                // Aqui cria-se as queues das actividades

                Uri compAddressIdentifyProds = host.GetSendAddress("pricerec-execute-activity-identifyproducts-compensate");

                h.ReceiveEndpoint(host, "pricerec-execute-activity-createtask", e =>
                {
                    e.ExecuteActivityHost<CreateTaskActivity, CreateTaskArguments>(); //() => new CreateTaskActivity()
                });


                //h.ReceiveEndpoint(host, "pricerec-execute-activity-identifyproducts", e =>
                //{
                //    e.ExecuteActivityHost<IdentifyProductsActivity, IdentifyProductsArguments>(compAddressIdentifyProds);
                //});

                //h.ReceiveEndpoint(host, "pricerec-execute-activity-identifyproducts-compensate", e =>
                //{
                //    e.CompensateActivityHost<IdentifyProductsActivity, IdentifyProductsLog>();
                //});

                // This ^ or this 


                h.ReceiveEndpoint(host, "pricerec-execute-activity-identifyproducts-compensate", e =>
                {
                    var compensateAddress = e.InputAddress;

                    e.ExecuteActivityHost<IdentifyProductsActivity, IdentifyProductsArguments>();

                    h.ReceiveEndpoint(host, "pricerec-execute-activity-identifyproducts", a =>
                    {
                        a.ExecuteActivityHost<IdentifyProductsActivity, IdentifyProductsArguments>(compensateAddress);
                    });

                });
            });
        }

        public void InitializeServiceBus()
        {
            try
            {
                this._busControl.StartAsync().Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}