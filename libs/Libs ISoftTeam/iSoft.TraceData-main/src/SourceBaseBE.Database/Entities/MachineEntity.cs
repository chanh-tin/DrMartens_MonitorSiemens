using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iSoft.Database.Entities;
using SourceBaseBE.Database.Interfaces;
using iSoft.Common.Enums;
using iSoft.Database.Extensions;

namespace SourceBaseBE.Database.Entities
{
    [Table("Machines")]
    public class MachineEntity : BaseCRUDEntity
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [NotMapped]
        public List<long>? OeePointIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(OeePointEntity), nameof(OeePointIds), EnumAttributeRelationshipType.Many2Many)]
        //[NotFormData]
        public List<OeePointEntity>? ListOeePoint { get; set; } = new();


        //[NotFormData]
        [ForeignKey(nameof(LineEntity))]
        public long? LineId { get; set; }
        public LineEntity? ItemLine { get; set; }
    }
}
