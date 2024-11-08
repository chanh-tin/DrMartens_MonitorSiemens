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
    [Table("Languages")]
    public class LanguageEntity : BaseCRUDEntity
    {
        [Required]
        [Column("Key")]
        public string? Key { get; set; }

        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        // https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes
        [StringLength(2)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, maxLen: 2)]
        public string? LanguageCode { get; set; }


        [NotMapped]
        public List<long>? TranslateLanguageIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(TranslateLanguageEntity), nameof(TranslateLanguageIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<TranslateLanguageEntity>? ListTranslateLanguage { get; set; } = new();


        [NotMapped]
        public List<long>? CountryIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(CountryEntity), nameof(CountryIds), EnumAttributeRelationshipType.Many2Many)]
        //[NotFormData]
        public List<CountryEntity>? ListCountry { get; set; } = new();


        [NotMapped]
        public List<long>? UserIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(UserEntity), nameof(UserIds), EnumAttributeRelationshipType.One2Many)]
        public List<UserEntity>? ListUser { get; set; } = new();
    }
}
