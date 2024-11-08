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
    public class BaseLanguageRequestModel : BaseCRUDRequestModel<LanguageEntity>
    {
        public virtual string? Key { get; set; }
        public virtual string Name { get; set; }
        public virtual string? LanguageCode { get; set; }
        public virtual List<long>? ListTranslateLanguage { get; set; }
        public virtual List<long>? ListCountry { get; set; }
        public virtual List<long>? ListUser { get; set; }
        
        public override LanguageEntity GetEntity(LanguageEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.Key != null) entity.Key = this.Key;
            if (this.Name != null) entity.Name = this.Name;
            if (this.LanguageCode != null) entity.LanguageCode = this.LanguageCode;
            if (this.ListTranslateLanguage != null)
            {
                entity.TranslateLanguageIds = this.ListTranslateLanguage.Select(x => x).ToList();
            }
            if (this.ListCountry != null)
            {
                entity.CountryIds = this.ListCountry.Select(x => x).ToList();
            }
            if (this.ListUser != null)
            {
                entity.UserIds = this.ListUser.Select(x => x).ToList();
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
