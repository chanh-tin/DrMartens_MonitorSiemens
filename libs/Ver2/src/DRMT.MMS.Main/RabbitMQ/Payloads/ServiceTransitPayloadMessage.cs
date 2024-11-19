using iSoft.Common.ExtensionMethods;
using iSoft.Common.Payloads;
using iSoft.Common.Utils;
using iSoft.AspNetCore.Enums;
using Newtonsoft.Json;
using SourceBaseBE.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SourceBaseBE.MainService.RabbitMQ.Payloads
{
    //public class ServiceTransitPayloadMessage : BaseMessage
    //{
    //    [JsonProperty("sender", NullValueHandling = NullValueHandling.Ignore)]
    //    public string Sender { get; set; }


    //    [JsonProperty("dest_service", NullValueHandling = NullValueHandling.Ignore)]
    //    public EnumDestService DestService { get; set; }


    //    [JsonProperty("dest_table", NullValueHandling = NullValueHandling.Ignore)]
    //    public EnumDestTable DestTable { get; set; }


    //    [JsonProperty("action", NullValueHandling = NullValueHandling.Ignore)]
    //    public EnumServiceTransitAction Action { get; set; }


    //    [JsonProperty("exe_at", NullValueHandling = NullValueHandling.Ignore)]
    //    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss.fffZ}")]
    //    public DateTime ExecuteAt { get; set; }


    //    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    //    public object Data { get; set; }



    //    public override string ToString()
    //    {
    //        return $"{{Sender: {Sender}, " +
    //            $"DestService: {DestService}, " +
    //            $"DestTable: {DestTable}, " +
    //            $"Action: {Action}, " +
    //            $"MessageId: {MessageId}, " +
    //            $"ExecuteAt: {ExecuteAt}, " +
    //            $"Data: {Data.ToJson()}" +
    //            $"}}";
    //    }

    //    public T GetDataObject<T>()
    //    {
    //        T entity = (T)Data;
    //        return entity;
    //    }
    //}
}
