using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Enums;

namespace SourceBaseBE.Database.Entities
{
    public class PermissionDetailEntity : BaseCRUDEntity
    {
        [DisplayField]
        public string PermissionTable { get; set; }


        public bool? View { get; set; }


        public bool? Create { get; set; }


        public bool? Edit { get; set; }


        public bool? Delete { get; set; }


        public bool? Request { get; set; }


        public bool? Approve { get; set; }


        [NotFormData]
        [ForeignKey(nameof(PermissionEntity))]
        public long? PermissionId { get; set; }
        public PermissionEntity? ItemPermission { get; set; }


    }
}
