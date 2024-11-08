using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using SourceBaseBE.Database.Helpers;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class UserResponseModel : BaseUserResponseModel
    {
        public Dictionary<string, List<string>> Permissions { get; set; }
        public void SetPermission(UserEntity entity)
        {
            this.Permissions = PermissionHelper.GetPermissions(entity.ListPermission, entity.ListUserGroup);
        }
    }
}
