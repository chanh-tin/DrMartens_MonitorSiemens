using System.Collections.Generic;
using System.Linq;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Entities;
using SourceBaseBE.Database.Enums;

namespace SourceBaseBE.MainService.ExtensionMethods
{
	public static class ExtensionMethods
	{
		public static IEnumerable<UserEntity> WithoutPasswords(this IEnumerable<UserEntity> users)
		{
			if (users == null) return null;

			return users.Select(x => x.WithoutPassword());
		}

		public static UserEntity WithoutPassword(this UserEntity user)
		{
			if (user == null) return null;

			user.Password = null;
			return user;
		}

        public static bool IsHasPermissionOn(this EnumUserRole requiredRole, string checkRole)
        {
            if (checkRole == null) return false;

            switch (requiredRole)
            {
                case EnumUserRole.None:
                    return true;
                case EnumUserRole.User:
                    if (checkRole.ToLower().Trim() == EnumUserRole.User.ToString().ToLower()
                      || checkRole.ToLower().Trim() == EnumUserRole.Admin.ToString().ToLower()
                      || checkRole.ToLower().Trim() == EnumUserRole.Root.ToString().ToLower())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case EnumUserRole.Admin:
                    if (checkRole.ToLower().Trim() == EnumUserRole.Admin.ToString().ToLower()
                      || checkRole.ToLower().Trim() == EnumUserRole.Root.ToString().ToLower())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case EnumUserRole.Root:
                    if (checkRole.ToLower().Trim() == EnumUserRole.Root.ToString().ToLower())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return true;
            }
        }
        public static bool IsHasPermissionOn(this PermissionEntity entity, string tableName, EnumCRUDType requiredCrudType)
        {
            if (tableName == "" && requiredCrudType == EnumCRUDType.None)
            {
                return true;
            }

            if (entity.ListPermissionDetail == null || entity.ListPermissionDetail.Count <= 0)
            {
                return false;
            }
            foreach (var permissionDetail in entity.ListPermissionDetail)
            {
                if (permissionDetail.PermissionTable.ToLower().Trim() == tableName.ToLower().Trim())
                {
                    switch (requiredCrudType)
                    {
                        case EnumCRUDType.None:
                            return true;
                        case EnumCRUDType.View:
                            if ((permissionDetail.View != null && permissionDetail.View.Value)
                                || (permissionDetail.Create != null && permissionDetail.Create.Value)
                                || (permissionDetail.Edit != null && permissionDetail.Edit.Value)
                                || (permissionDetail.Delete != null && permissionDetail.Delete.Value)
                                || (permissionDetail.Request != null && permissionDetail.Request.Value)
                                || (permissionDetail.Approve != null && permissionDetail.Approve.Value))
                            {
                                return true;
                            }
                            break;
                        case EnumCRUDType.Create:
                            if ((permissionDetail.Create != null && permissionDetail.Create.Value)
                                || (permissionDetail.Edit != null && permissionDetail.Edit.Value)
                                || (permissionDetail.Delete != null && permissionDetail.Delete.Value)
                                || (permissionDetail.Request != null && permissionDetail.Request.Value)
                                || (permissionDetail.Approve != null && permissionDetail.Approve.Value))
                            {
                                return true;
                            }
                            break;
                        case EnumCRUDType.Edit:
                            if ((permissionDetail.Edit != null && permissionDetail.Edit.Value)
                                || (permissionDetail.Delete != null && permissionDetail.Delete.Value)
                                || (permissionDetail.Approve != null && permissionDetail.Approve.Value))
                            {
                                return true;
                            }
                            break;
                        case EnumCRUDType.Delete:
                            if ((permissionDetail.Delete != null && permissionDetail.Delete.Value)
                                || (permissionDetail.Approve != null && permissionDetail.Approve.Value))
                            {
                                return true;
                            }
                            break;
                        case EnumCRUDType.Request:
                            if ((permissionDetail.Request != null && permissionDetail.Request.Value))
                            {
                                return true;
                            }
                            break;
                        case EnumCRUDType.Approve:
                            if ((permissionDetail.Approve != null && permissionDetail.Approve.Value))
                            {
                                return true;
                            }
                            break;
                    }
                }
            }
            return false;
        }
    }
}