using iSoft.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool? SyncConfigFlag { get; set; } = false;


        [NotMapped]
        public List<long>? OeePointIds { get; set; } = new List<long>();
        //[ListEntityAttribute(nameof(OeePointEntity), nameof(OeePointIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<OeePointEntity>? ListOeePoint { get; set; } = new();
    }
}
