using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;
using MathNet.Numerics.Differentiation;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("Example002s")]
    public class Example002Entity : BaseCRUDEntity
    {

        [NotMapped]
        public List<long>? Example001Ids { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(Example001Entity), nameof(Example001Ids), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<Example001Entity>? ListExample001 { get; set; } = new();

    }
}
