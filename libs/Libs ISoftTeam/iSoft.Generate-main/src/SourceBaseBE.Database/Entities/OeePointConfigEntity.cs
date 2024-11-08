using InfluxDB.Client.Api.Domain;
using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;
using MathNet.Numerics.Differentiation;
using Microsoft.AspNetCore.Mvc.Formatters;
using SourceBaseBE.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("OeePointConfigs")]
    public class OeePointConfigEntity : BaseCRUDEntity
    {
        public string? TotalCountInTag { get; set; }
        public string? TotalGoodCountTag { get; set; }
        public string? TotalNGCountTag { get; set; }
        public string? AvaiableTag { get; set; }
        public string? PerformanceTag { get; set; }
        public string? QualityTag { get; set; }
        public string? OeeTag { get; set; }
        public string? TotalDowntimeTag { get; set; }
        public string? TotalRunTimeTag { get; set; }
        public string? OeePointStatusTag { get; set; }
        public string? CurrentDurationTag { get; set; }
        public bool? IsConfigSyncedFlag { get; set; } = false;
        public DateTime? SyncDataBeginDate { get; set; }
        public bool? IsDataSyncedFlag { get; set; } = false;


        [NotMapped]
        public List<long>? OeePointIds { get; set; } = new List<long>();
        //[ListEntityAttribute(nameof(OeePointEntity), nameof(OeePointIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<OeePointEntity>? ListOeePoint { get; set; } = new();
    }
}
