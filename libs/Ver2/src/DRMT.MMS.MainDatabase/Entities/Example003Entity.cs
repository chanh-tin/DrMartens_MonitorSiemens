using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("I_Example003s")]
    public class Example003Entity : BaseCRUDEntity
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [NotMapped]
        public List<long>? Example001Ids { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(Example001Entity), nameof(Example001Ids), EnumAttributeRelationshipType.Many2Many)]
        //[NotFormData]
        public List<Example001Entity>? ListExample001 { get; set; } = new();
    }
}
