using iSoft.Common;
using iSoft.Common.Exceptions;
using iSoft.Common.Models.RequestModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using iSoft.Redis.Services;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using iSoft.Database.Models;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Common.Utils;


namespace SourceBaseBE.Database.Repository
{
    public class Example002Repository : BaseExample002Repository
    {
        public Example002Repository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override string GetRepositoryName()
        {
            return nameof(Example002Repository);
        }
    }
}
