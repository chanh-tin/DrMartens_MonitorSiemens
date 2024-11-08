// -----------------------------------------------------------------------------
// This file was automatically generated.
// Please do not edit this file manually.
//
// Generated Date: 
//
// -----------------------------------------------------------------------------

using iSoft.Common.Utils;
using Microsoft.AspNetCore.Http;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class BaseCountryRequestModel : BaseCRUDRequestModel<CountryEntity>
    {
        public virtual string Name { get; set; }
        public virtual string? Currency { get; set; }
        public virtual string? InternationalDialingCode { get; set; }
        public virtual List<long>? ListLanguage { get; set; }
        public virtual List<long>? ListUser { get; set; }
        public virtual List<long>? ListTimezone { get; set; }
        
        public override CountryEntity GetEntity(CountryEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.Name != null) entity.Name = this.Name;
            if (this.Currency != null) entity.Currency = this.Currency;
            if (this.InternationalDialingCode != null) entity.InternationalDialingCode = this.InternationalDialingCode;
            if (this.ListLanguage != null)
            {
                entity.LanguageIds = this.ListLanguage.Select(x => x).ToList();
            }
            if (this.ListUser != null)
            {
                entity.UserIds = this.ListUser.Select(x => x).ToList();
            }
            if (this.ListTimezone != null)
            {
                entity.TimezoneIds = this.ListTimezone.Select(x => x).ToList();
            }
        
            return entity;
        }

        public override Dictionary<string, (string, IFormFile)> GetFiles()
        {
            Dictionary<string, (string, IFormFile)> dicRS = new Dictionary<string, (string, IFormFile)>();
            
            
            return dicRS;
        }
    }
}