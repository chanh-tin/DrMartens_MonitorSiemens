using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Models.ResponseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceBaseBE.Database.Entities
{
    [Table("I_Example001Trans")]
    public class Example001TransEntity : BaseCRUDTransEntity<Example001Entity>
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [StringLength(510)]
        [FormDataTypeAttributeText(EnumFormDataType.Textarea, maxLen: 510)]
        public string? Description { get; set; }

        public virtual void ApplyTransDataTo(BaseExample001ResponseModel responseModel)
        {
            responseModel.Name = Name;
            responseModel.Description = Description;
        }

        public virtual void ApplyTransDataTo(Example001Entity entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
