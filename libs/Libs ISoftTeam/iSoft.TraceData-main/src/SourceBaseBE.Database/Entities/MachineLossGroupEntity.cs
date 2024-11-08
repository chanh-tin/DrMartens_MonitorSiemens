using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;

using Microsoft.AspNetCore.Mvc.Formatters;
using SourceBaseBE.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("MachineLossGroups")]
    public class MachineLossGroupEntity : BaseCRUDEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public EnumOeeAPQType? OeeAPQType { get; set; }


        [NotMapped]
        public List<long>? MachineLossIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(MachineLossEntity), nameof(MachineLossIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<MachineLossEntity>? ListMachineLoss { get; set; } = new();
    }
}
