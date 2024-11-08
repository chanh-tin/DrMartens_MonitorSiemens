using iSoft.Common;
using iSoft.Common.Enums;
using iSoft.DBLibrary.Entities;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSoft.Database.Entities
{
    [Table("FCM")]
    public class FCMEntity: BaseCRUDEntity
  {
        [Required]
        public long UserId { get; set; }


        [StringLength(255)]
        public string Token { get; set; }


        [DefaultValue(EnumEnableFlag.Enabled)]
        public EnumEnableFlag EnableFlag { get; set; }
    }
}