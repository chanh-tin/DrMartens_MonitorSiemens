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
    [Table("Timezones")]
    public class TimezoneEntity : BaseCRUDEntity
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [FormDataTypeAttributeNumber(EnumFormDataType.IntegerNumber, min: -12, max: 14, defaultVal: 7, unit: "")]
        public int? Timezone { get; set; }


        [NotMapped]
        public List<long>? CountryIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(CountryEntity), nameof(CountryIds), EnumAttributeRelationshipType.Many2Many)]
        //[NotFormData]
        public List<CountryEntity>? ListCountry { get; set; } = new();
    }
}
