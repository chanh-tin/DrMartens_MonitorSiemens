using iSoft.Common.Enums;

namespace iMag.Oee.Models.RequestModels
{
    public class ConnectionTagDataModel
    {
        public string? Id { get; set; }
        public long? EnvKey { get; set; }
        public string? Name { get; set; }    
        public string ConnectionId { get; set; }  
        public EnumReadWrite? ReadWrite { get; set; }
        public EnumPoolType? PoolType { get; set; }  
        public string? UnitMeasuring { get; set; }
        public EnumDataType? Type { get; set; }
        public int? ReadInterval { get ; set; }
        public string? Category { get; set; }
        public float? Value { get; set; }
    }
}
