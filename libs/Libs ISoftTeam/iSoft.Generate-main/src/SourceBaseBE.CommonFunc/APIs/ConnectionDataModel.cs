using iSoft.Common.Enums;

namespace iMag.Oee.Models.RequestModels
{
    public class ConnectionDataModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ConnProtocolType { get; set; }
        public string? ConnType { get; set; }
        public string? Category { get; set; }
    }
}
