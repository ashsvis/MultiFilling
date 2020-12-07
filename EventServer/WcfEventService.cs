using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace MultiFilling.EventServer
{
    /*
     * Класс реализации запуска WCF-сервиса. 
     * Реализован с использованием шаблона Singleton
     */
    public sealed class WcfEventService
    {
        private readonly TimeSpan _timeout = new TimeSpan(0, 1, 30);
        private static WcfEventService _wcfEventService;
        private readonly ServiceHost _svcHost;

        public static WcfEventService EventService
        {
            get
            {
                _wcfEventService = _wcfEventService ?? new WcfEventService();
                return _wcfEventService;
            }
        }

        // Конструктор по умолчанию определяется как private
        private WcfEventService()
        {
            // Регистрация сервиса и его метаданных
            _svcHost = new ServiceHost(typeof(AShEventService),
                                       new[]
                                           {
                                               new Uri("net.pipe://localhost/FillingEventServer"),
                                               new Uri("net.tcp://localhost:9901/FillingEventServer")
                                           });
            _svcHost.AddServiceEndpoint(typeof(IAShEventService),
                                        new NetNamedPipeBinding(), "");
            _svcHost.AddServiceEndpoint(typeof(IAShEventService),
                                        new NetTcpBinding
                                        {
                                            OpenTimeout = _timeout,
                                            SendTimeout = _timeout,
                                            ReceiveTimeout = _timeout,
                                            CloseTimeout = _timeout,
                                            Security = new NetTcpSecurity {Mode = SecurityMode.None }
                                        }, "");
            var behavior = new ServiceMetadataBehavior();
            _svcHost.Description.Behaviors.Add(behavior);
            _svcHost.AddServiceEndpoint(typeof(IMetadataExchange),
                                        MetadataExchangeBindings.CreateMexNamedPipeBinding(), "mex");
            _svcHost.AddServiceEndpoint(typeof(IMetadataExchange),
                                        MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
        }

        public void Start()
        {
            _svcHost.Open();
        }

        public void Stop()
        {
            _svcHost.Close();
        }
    }

}
