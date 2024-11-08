using InfluxDB.Client.Api.Domain;
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
    [Table("OeePoints")]
    public class OeePointEntity : BaseCRUDEntity
    {
        //public long? LineId { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
        public string? Description { get; set; }
        public string? TagNames { get; set; }
        public int? IdealRunRate { get; set; }
        public int? IdealCycleTime { get; set; }

        //[NotFormData]
        [ForeignKey(nameof(OeePointConfigEntity))]
        public long? OeePointConfigId { get; set; }
        public OeePointConfigEntity? ItemOeePointConfig { get; set; }


        [NotMapped]
        public List<long>? MachineBlockDataIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(MachineBlockDataEntity), nameof(MachineBlockDataIds), EnumAttributeRelationshipType.One2Many)]
        //[NotFormData]
        public List<MachineBlockDataEntity>? ListMachineBlockData { get; set; } = new();


        //[NotFormData]
        [ForeignKey(nameof(LineEntity))]
        public long? LineId { get; set; }
        public LineEntity? ItemLine { get; set; }


        [NotMapped]
        public List<long>? MachineIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(MachineEntity), nameof(MachineIds), EnumAttributeRelationshipType.Many2Many)]
        //[NotFormData]
        public List<MachineEntity>? ListMachine { get; set; }
    }
}
