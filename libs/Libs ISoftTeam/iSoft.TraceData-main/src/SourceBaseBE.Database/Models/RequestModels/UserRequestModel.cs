using iSoft.Common.Utils;
using Microsoft.AspNetCore.Http;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class UserRequestModel : BaseUserRequestModel
    {
        public new string? Username { get; set; }
        public new string? Password { get; set; }
        public new string? Role { get; set; }
    }
}
