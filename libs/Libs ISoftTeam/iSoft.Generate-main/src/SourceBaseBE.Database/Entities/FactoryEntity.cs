using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Interfaces;

namespace SourceBaseBE.Database.Entities
{
    public class FactoryEntity : BaseCRUDEntity
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [NotMapped]
        public List<long>? WorkshopIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(WorkshopEntity), nameof(WorkshopIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<WorkshopEntity>? ListWorkshop { get; set; } = new();
    }
}
