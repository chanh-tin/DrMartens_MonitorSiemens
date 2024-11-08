using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SourceBaseBE.Database.Entities
{
  public class WorkshopEntity:BaseCRUDEntity
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [NotMapped]
        public List<long>? LineIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(LineEntity), nameof(LineIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<LineEntity>? ListLine { get; set; } = new();


        //[NotFormData]
        [ForeignKey(nameof(FactoryEntity))]
        public long? FactoryId { get; set; }
        public FactoryEntity? ItemFactory { get; set; }
    }
}
