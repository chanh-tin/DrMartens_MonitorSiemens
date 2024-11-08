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
    [Table("Countries")]
    public class CountryEntity : BaseCRUDEntity
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [StringLength(3)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, maxLen: 3)]
        public string? Currency {  get; set; }


        [StringLength(5)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, maxLen: 5)]
        public string? InternationalDialingCode { get; set; }


        [NotMapped]
        public List<long>? LanguageIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(LanguageEntity), nameof(LanguageIds), EnumAttributeRelationshipType.Many2Many)]
        //[NotFormData]
        public List<LanguageEntity>? ListLanguage { get; set; } = new();


        [NotMapped]
        public List<long>? UserIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(UserEntity), nameof(UserIds), EnumAttributeRelationshipType.One2Many)]
        public List<UserEntity>? ListUser { get; set; } = new();


        [NotMapped]
        public List<long>? TimezoneIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(TimezoneEntity), nameof(TimezoneIds), EnumAttributeRelationshipType.Many2Many)]
        //[NotFormData]
        public List<TimezoneEntity>? ListTimezone { get; set; } = new();
    }
}
