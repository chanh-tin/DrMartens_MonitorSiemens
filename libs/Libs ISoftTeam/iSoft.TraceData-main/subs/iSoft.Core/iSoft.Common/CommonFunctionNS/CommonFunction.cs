using iSoft.Common.Enums;
using iSoft.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Common.CommonFunctionNS
{
    public class CommonFunction
    {

        public static long? GetCurrentUserId(HttpContext httpContext)
        {
            if (httpContext == null || httpContext.User == null)
            {
                return null;
            }

            if (httpContext.User.Claims == null)
            {
                return null;
            }

            try
            {
                var listItem = httpContext.User.Claims.Where(x => x.Type.ToLower() == EnumIdentityType.UserId.ToString().ToLower());
                if (listItem.Any())
                {
                    return long.Parse(listItem.FirstOrDefault().Value);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetCurrentUserRole(HttpContext httpContext)
        {
            if (httpContext == null || httpContext.User == null)
            {
                return null;
            }

            if (httpContext.User.Claims == null)
            {
                return null;
            }

            try
            {
                var listItem = httpContext.User.Claims.Where(x => x.Type.ToLower() == EnumIdentityType.Role.ToString().ToLower());
                if (listItem.Any())
                {
                    return listItem.FirstOrDefault().Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static Object? GetCurrentUser(HttpContext httpContext)
        {
            if (httpContext == null || httpContext.Items == null)
            {
                return null;
            }
            if (httpContext.Items.ContainsKey(EnumIdentityType.CurrentUser.ToString().ToLower()))
            {
                return httpContext.Items[EnumIdentityType.CurrentUser.ToString().ToLower()];
            }
            return null;
        }
        public static List<long> GetHasPermissionDepartmentIds(HttpContext httpContext)
        {
            if (httpContext == null || httpContext.Items == null)
            {
                return new List<long>();
            }
            if (httpContext.Items.ContainsKey(EnumIdentityType.ListHasPermissionDepartmentId.ToString().ToLower()))
            {
                return (List<long>)httpContext.Items[EnumIdentityType.ListHasPermissionDepartmentId.ToString().ToLower()];
            }
            return new List<long>();
        }
        public static EnumDepartmentAdmin GetCurrentCheckDepartmentAdminRole(HttpContext httpContext)
        {
            if (httpContext == null || httpContext.Items == null)
            {
                return EnumDepartmentAdmin.Admin3;
            }
            if (httpContext.Items.ContainsKey(EnumIdentityType.CurrentCheckDepartmentAdminRole.ToString().ToLower()))
            {
                return (EnumDepartmentAdmin)httpContext.Items[EnumIdentityType.CurrentCheckDepartmentAdminRole.ToString().ToLower()];
            }
            return EnumDepartmentAdmin.Admin3;
        }
        public static bool IsHasPermissionDepartmentIds(HttpContext httpContext, long departmentId)
        {
            if (GetCurrentCheckDepartmentAdminRole(httpContext) == EnumDepartmentAdmin.None)
            {
                return true;
            }
            var listId = GetHasPermissionDepartmentIds(httpContext);
            if (listId.Contains(departmentId))
            {
                return true;
            }
            return false;
        }
    }
}
