using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class MachineLossDataModel 
    {
        public MachineLossModel? MachineLossData { get; set; }
        public long? DurationInMiliSeconds { get; set; }
    }
}
