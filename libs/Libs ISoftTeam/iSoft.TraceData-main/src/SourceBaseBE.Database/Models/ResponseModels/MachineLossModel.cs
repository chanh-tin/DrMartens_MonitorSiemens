using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class MachineLossModel
    {
        public string? LossName { get; set; }
        public string? LossPosition { get; set; }
        public string? LossReason { get; set; }
        public string? LossDescription { get; set; }
        public string? DowntimeType { get; set; }
        public string? Note { get; set; }
        public long? Order { get; set; }
    }
}
