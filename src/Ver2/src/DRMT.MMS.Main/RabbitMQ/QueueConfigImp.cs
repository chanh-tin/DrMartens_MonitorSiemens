using iSoft.AspNetCore.RabbitMQ;
using iSoft.RabbitMQ;

namespace SourceBaseBE.MainService.RabbitMQ
{
    public class QueueConfigImp: BaseQueueConfig
    {
        private static QueueConfigImp _instance = null;
        public static QueueConfigImp Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QueueConfigImp();
                }
                return _instance;
            }
        }
        private QueueConfigImp()
        {
        }

        public override string ServiceTransitTopic { get { return "ServiceTransit_DRMTMMSComputeTopic"; } }
        public override QueueProperties ServiceTransitTopicProperty
        {
            get { return new QueueProperties(ServiceTransitTopic, ServiceTransitEx).SetRetryQueue(30, 50, 8, true); }
        }


        public string OEEDataInputTopic { get { return "OEEDataInputTopic"; } }
        public QueueProperties OEEDataInputTopicProperty
        {
            get { return new QueueProperties(OEEDataInputTopic, OEEDataInputTopic).SetRetryQueue(30, 50, 8, true); }
        }
    }
}
