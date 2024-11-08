using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iSoft.Database.Entities;
using SourceBaseBE.Database.Interfaces;
using iSoft.Common.Enums;
using iSoft.Database.Extensions;

namespace SourceBaseBE.Database.Entities
{
    [Table("Lines")]
    public class LineEntity : BaseCRUDEntity
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [NotMapped]
        public List<long>? OeePointIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(OeePointEntity), nameof(OeePointIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<OeePointEntity>? ListOeePoint { get; set; } = new();


        [NotMapped]
        public List<long>? MachineIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(MachineEntity), nameof(MachineIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<MachineEntity>? ListMachine { get; set; } = new();


        [NotMapped]
        public List<long>? BreakTimeIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(BreakTimeEntity), nameof(BreakTimeIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<BreakTimeEntity>? ListBreakTime { get; set; } = new();


        [NotMapped]
        public List<long>? ShiftIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(ShiftEntity), nameof(ShiftIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<ShiftEntity>? ListShift { get; set; } = new();


        //[NotFormData]
        [ForeignKey(nameof(WorkshopEntity))]
        public long? WorkshopId { get; set; }
        public WorkshopEntity? ItemWorkshop { get; set; }


    }
}
