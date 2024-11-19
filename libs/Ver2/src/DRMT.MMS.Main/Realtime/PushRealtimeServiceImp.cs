using iSoft.SocketIOClient.Services;

namespace SourceBaseBE.MainService.Realtime
{
    public class PushRealtimeServiceImp : PushRealtimeService
    {
        private static PushRealtimeServiceImp _instance = null;
        public static PushRealtimeServiceImp Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PushRealtimeServiceImp();
                }
                return _instance;
            }
        }
    }
}