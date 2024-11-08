using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Models.ResponseModels;
using LinqKit;
using SourceBaseBE.Database.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class MachineBlockDataModel
    {
        public string? Line { get; set; }
        public string? LossName { get; set; }
        public string? LossPosition { get; set; }
        public string? LossDescription { get; set; }
        public EnumMachineStatusType? StatusType { get; set; }
        public string? Note { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public long? DurationInMiliSeconds { get; set; }
    }
}
