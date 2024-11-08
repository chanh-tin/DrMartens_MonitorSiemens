using iSoft.Common.Enums;
using iSoft.Database.Extensions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSoft.Database.Entities
{
    public class BaseCRUDEntity
    {
        [Key]
        [FormDataTypeAttributeNumber(EnumFormDataType.Hidden)]
        public long Id { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Label)]
        public long? CreatedBy { get; set; }


        [NotMapped]
        [StringLength(50)]
        [FormDataTypeAttributeText(EnumFormDataType.Label)]
        public string? CreatedUsername { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Label)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss.fff}")]
        public DateTime? CreatedAt { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Label)]
        public long? UpdatedBy { get; set; }


        [NotMapped]
        [StringLength(50)]
        [FormDataTypeAttributeText(EnumFormDataType.Label)]
        public string? UpdatedUsername { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Label)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss.fff}")]
        public DateTime? UpdatedAt { get; set; }


        [FormDataTypeAttributeSelect(EnumFormDataType.Checkbox, null, isReadonly: true)]
        public bool? DeletedFlag { get; set; }


        [FormDataTypeAttributeNumber(EnumFormDataType.IntegerNumber, min: 1, max: 1000, defaultVal: 1)]
        public long? Order { get; set; }
    }
}