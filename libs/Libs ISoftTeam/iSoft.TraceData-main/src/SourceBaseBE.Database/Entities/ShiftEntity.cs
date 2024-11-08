using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;

using Microsoft.AspNetCore.Mvc.Formatters;
using SourceBaseBE.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("Shifts")]
    public class ShiftEntity : BaseCRUDEntity
    {
        public string StartTime { get; set; }


        public string EndTime { get; set; }


        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public EnumShiftType? ShiftType { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(LineEntity))]
        public long? LineId { get; set; }
        public LineEntity? ItemLine { get; set; }

        public override string ToString()
        {
            return $"[{ShiftType} | {StartTime} -> {EndTime}]";
        }
    }
}
