using iSoft.Common.Enums;
using iSoft.Database.Extensions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSoft.Database.Entities
{
    [Table("Users")]
    public class UserEntity : BaseCRUDEntity
    {
        [DisplayField]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 50)]
        [StringLength(50)]
        public string Username { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Password, isRequired: true, maxLen: 50)]
        [StringLength(255)]
        public string Password { get; set; }


        [StringLength(10)]
        public string Role { get; set; }


        public EnumEnableFlag? EnableFlag { get; set; } = EnumEnableFlag.Enabled;


        [NotMapped]
        public List<long>? ISoftProjectIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(ISoftProjectEntity), nameof(ISoftProjectIds), EnumAttributeRelationshipType.Many2Many)]
        public List<ISoftProjectEntity>? ListISoftProject { get; set; } = new();
    }
}