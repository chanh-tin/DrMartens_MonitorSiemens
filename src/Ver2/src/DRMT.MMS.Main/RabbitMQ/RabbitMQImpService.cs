using iSoft.Common.ConfigsNS;
using iSoft.RabbitMQ.Services;
using iSoft.AspNetCore.RabbitMQ;

namespace SourceBaseBE.MainService.RabbitMQ
{
    public class RabbitMQImpService: BaseRabbitMQImpService
    {
        private static RabbitMQImpService _instance = null;
        public static RabbitMQImpService Instance
        {
            get
            {
                if (_instance == null)
                {
                    var rabbitMQService = new RabbitMQService(CommonConfig.RabbitMQConfig);
                    _instance = new RabbitMQImpService(rabbitMQService);
                }
                return _instance;
            }
        }
        private RabbitMQImpService(RabbitMQService rabbitMQService)
            : base(rabbitMQService)
        {
        }
    }
}
