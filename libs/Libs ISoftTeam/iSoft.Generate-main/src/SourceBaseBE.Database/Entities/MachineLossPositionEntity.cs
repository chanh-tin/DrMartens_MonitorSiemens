using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;
using MathNet.Numerics.Differentiation;
using Microsoft.AspNetCore.Mvc.Formatters;
using SourceBaseBE.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("MachineLossPositions")]
    public class MachineLossPositionEntity : BaseCRUDEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }


        [NotMapped]
        public List<long>? MachineBlockDataIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(MachineBlockDataEntity), nameof(MachineBlockDataIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<MachineBlockDataEntity>? ListMachineBlockData { get; set; } = new();
    }
}
