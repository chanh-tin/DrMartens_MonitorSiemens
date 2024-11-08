using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using iSoft.Common.CommonFunctionNS;
using iSoft.Common.Enums;
using iSoft.Common.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SourceBaseBE.Database.Entities;
using SourceBaseBE.Database.Enums;
using SourceBaseBE.Database.Helpers;
using SourceBaseBE.MainService.ExtensionMethods;
using SourceBaseBE.MainService.Services;

namespace SourceBaseBE.MainService.CustomAttributes
{
    /// <summary>
    /// *** Attribute Example: ***
    /// [UserPermission]
    /// [UserPermission(EnumUserRole.Admin)]
    /// [UserPermission(EnumUserRole.Root)]
    /// [UserPermission("profile", Database.Enums.EnumCRUDType.Edit)]
    /// [UserPermission("user", Database.Enums.EnumCRUDType.Delete)]
    /// [UserPermission("group1")]
    /// [UserPermission("group1", "user", Database.Enums.EnumCRUDType.Delete)]
    /// 
    /// *** Get current user in controller example: ***
    /// var currentUserId = CommonFunction.GetCurrentUserId(this.HttpContext);
    /// UserEntity? currentUser = (UserEntity)CommonFunction.GetCurrentUser(this.HttpContext);
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UserPermissionAttribute : Attribute, IAuthorizationFilter
    {
        private string _tableName { get; set; } = "";
        private EnumCRUDType _crudType { get; set; } = EnumCRUDType.None;
        private EnumUserRole _userRole { get; set; } = EnumUserRole.None;
        private string _groupName { get; set; } = "";

        public UserPermissionAttribute()
        {
        }
        public UserPermissionAttribute(EnumUserRole userRole)
        {
            _userRole = userRole;
        }
        public UserPermissionAttribute(string tableName, EnumCRUDType crudType)
        {
            this._tableName = tableName;
            this._crudType = crudType;
        }
        public UserPermissionAttribute(string groupName)
        {
            this._groupName = groupName;
        }
        public UserPermissionAttribute(string groupName, string tableName, EnumCRUDType crudType)
        {
            this._groupName = groupName;
            this._tableName = tableName;
            this._crudType = crudType;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            long? userId0 = CommonFunction.GetCurrentUserId(context.HttpContext);
            if (userId0 == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userId = userId0.Value;

            var _userService = context.HttpContext.RequestServices.GetService<UserService>();

            var currUser = _userService.GetProfile(userId);

            if (currUser == null)
            {
                //context.Result = new UnauthorizedResult();
                //return;
            }
            else
            {
                if (!context.HttpContext.Items.ContainsKey(EnumIdentityType.CurrentUser.ToString().ToLower()))
                {
                    context.HttpContext.Items.Add(EnumIdentityType.CurrentUser.ToString().ToLower(), currUser);
                }
                else
                {
                    context.HttpContext.Items[EnumIdentityType.CurrentUser.ToString().ToLower()] = currUser;
                }
            }

            // Check UserRole
            var userRole = CommonFunction.GetCurrentUserRole(context.HttpContext);
            if (!this._userRole.IsHasPermissionOn(userRole))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check Group
            if (!string.IsNullOrEmpty(this._groupName))
            {
                if (currUser == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

            }

            // Check Permission
            if (this._crudType != EnumCRUDType.None && this._tableName != "")
            {
                if (currUser == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                List<PermissionEntity> listPermission = new List<PermissionEntity>();

                if (currUser.ListPermission != null && currUser.ListPermission.Count >= 1)
                {
                    listPermission.AddRange(currUser.ListPermission);
                }

                foreach (var userGroup in currUser.ListUserGroup)
                {
                    listPermission.AddRange(userGroup.ListPermission);
                }

                listPermission = ConvertUtil.RemoveDuplicateBy(listPermission, x=>x.Id);
                foreach (var permission in listPermission)
                {
                    if (permission.IsHasPermissionOn(this._tableName, this._crudType))
                    {
                        return;
                    }
                }

                context.Result = new UnauthorizedResult();
                return;
            }

            return;
        }
    }
}
