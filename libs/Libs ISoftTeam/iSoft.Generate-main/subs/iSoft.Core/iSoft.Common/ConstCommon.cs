using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Common
{
    public class ConstCommon
    {
        public const int ConstSelectListMaxRecord = 1000;
        public const int ConstExportInDayMaxRecord = 9999;

        public const string ConstSourceBaseBECacheMainService = "SourceBaseBE|Main";
        public const string ConstISoftCacheMainService = "ISoft|Main";

        public const int ConstGetListCacheExpiredTimeInSeconds = 600;
        public const int ConstGetListNotIncludeCacheExpiredTimeInSeconds = 600;
        public const int ConstGetDetailCacheExpiredTimeInSeconds = 600;

        public const long ConstDeltaSeconds = 5 * 60;
        public const string ConstSeperatorFileKey = "_|_";

        public const int MINIMUM_LENGTH_PASSWORD = 6;
        public const int MAXIMUM_LENGTH_PASSWORD = 40;

        public const int NUMBER_TOP_LOSSTIME = 5;
        public const int NUMBER_TOTAL_LOSSTIME = 500;

        public class ConstDateTimeFormat
        {
            public const string DDMMYYYY_Flash = "dd/MM/yyyy";
            public const string DDMMYYYY = "dd-MM-yyyy";
            public const string DDMMYYYY_HHMMSS = "dd-MM-yyyy HH:mm:ss";
            public const string YYYYMMDD = "yyyy-MM-dd";
            public const string HHMMSS = "HH:mm:ss";
            public const string YYYYMMDD_HHMMSS = "yyyy-MM-dd HH:mm:ss";
            public const string YYYYMMDDTHHMMSS_FFFZ = "yyyy-MM-ddTHH:mm:ss.fffZ";
            public const string YYYYMMDDTHHMMSS_FFF = "yyyy-MM-ddTHH:mm:ss.fff";
            public const string YYYYMMDDTHHMMSSFFF_07_00 = "yyyy-MM-ddTHH:mm:ss.fff+07:00";
            public const string YYYYMMDD_HHMMSSFFF = "yyyy-MM-dd HH:mm:ss.fff";
            public const string YYYYMMDD_HHMMSS_SSS = "yyyy-MM-dd HH:mm:ss.SSS";
            public const string YYYYMMDDTHHMMSS_SSS = "yyyy-MM-dd'T'HH:mm:ss.SSS";
            public const string YYYYMMDD_HHMMSS_MIN = "yyyyMMdd_HHmmss";
        }
        public class ConstFolderPath
        {
            public const string Root = "wwwroot";
            public const string Images = "Images";
            public const string Files = "Files";
            public const string Upload = "Upload";
        }
        public class ConstFirebase
        {
            public const string FCMSendURL = "https://fcm.googleapis.com/fcm/send";
        }

        public const long ConstDisconnectIntervalInMiliSeconds = 60000;
        public const long ConstTimeOutCounterIntervalInMiliSeconds = 30000;
    }
}
