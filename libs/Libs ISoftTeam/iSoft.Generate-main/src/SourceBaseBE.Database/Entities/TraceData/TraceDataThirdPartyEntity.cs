//using System.Text.Json.Serialization;
//using System.Linq;
//using System.IO;
//using System;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using iSoft.DBLibrary.Entities;
//using SourceBaseBE.Database.DTOs;
//using System.Data;
//using iSoft.Common.Payloads;
//using System.Reflection;
//using iSoft.Common.Utils;
//
//using iSoft.Common.Exceptions;

//namespace SourceBaseBE.Database.Entities.TraceData
//{
//  [Table("TraceDataThirdPartyEntitys")]
//  public class TraceDataThirdPartyEntity : BaseTraceDataEntity
//  {
//    public TraceDataThirdPartyEntity() { }
//    public TraceDataThirdPartyEntity(long? connectionId, 
//                            object?[] arrValue, 
//                            DateTime executeAt, 
//                            string messageId, 
//                            Dictionary<string, EnvironmentEntity> dicEnvEntity)
//      : base(connectionId, arrValue, executeAt, messageId, dicEnvEntity)
//    {
//    }

//  }
//}