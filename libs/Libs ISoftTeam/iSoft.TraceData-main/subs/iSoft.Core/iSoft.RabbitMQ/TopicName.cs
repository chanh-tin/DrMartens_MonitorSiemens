#define VIRTUAL_MODEx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.RabbitMQ
{
    public class TopicName
    {

#if VIRTUAL_MODE
#else
#endif

#if VIRTUAL_MODE
        public static string TraceDataTopic = "TraceDataTopic";
        public static string SearchDataTopic = "SearchDataTopic";
        public static string RealtimeDataTopic = "RealtimeDataTopic";
        public static string TrackDeviceTopic = "TrackDeviceTopic";
        public static string SyncDataTopic = "SyncDataTopic";
        public static string UpdateWorkingDayTopic = "UpdateWorkingDayTopic";
        public static string OEEDataInputTopic = "OEEDataInputTopic";
#else
        public static string TraceDataTopic = "TraceDataTopic";
        public static string SearchDataTopic = "SearchDataTopic";
        public static string RealtimeDataTopic = "RealtimeDataTopic";
        public static string TrackDeviceTopic = "TrackDeviceTopic";
        public static string SyncDataTopic = "SyncDataTopic";
        public static string OEEDataInputTopic = "OEEDataInputTopic";
#endif
    }
    public class ExchangeName
    {
#if VIRTUAL_MODE
        public static string SourceBaseBEEnvEx = "SourceBaseBEEnvEx";
        public static string SourceBaseBESyncDataEx = "SourceBaseBESyncDataEx";
#else
        public static string SourceBaseBEEnvEx = "ConnectivityEx";
        public static string SourceBaseBESyncDataEx = "SourceBaseBESyncDataEx";
#endif
    }
}
