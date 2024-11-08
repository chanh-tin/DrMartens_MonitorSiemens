//using iSoft.Common.Models.RemoteConfigModels;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using Newtonsoft.Json;
using SourceBaseBE.Database.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Entities
{
    [Table("TranslateLanguages")]
    public class TranslateLanguageEntity : BaseCRUDEntity
    {
        [DisplayField]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string? Key { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string? DisplayName { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textbox, maxLen: 50)]
        public EnumLanguageCategory? Category { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(LanguageEntity))]
        public long? LanguageId { get; set; }
        public LanguageEntity? ItemLanguage { get; set; }
    }
}
