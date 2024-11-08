using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Helpers
{
    public class PermissionHelper
    {
        public static DateTime GetStartOfDate(DateTime start)
        {
            return new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
        }

        public static Dictionary<string, List<string>> GetPermissions(
            List<PermissionEntity>? listPermission,
            List<UserGroupEntity>? listPermissionGroup)
        {
            var dicRS = new Dictionary<string, List<string>>();
            foreach (var permission in listPermission)
            {
                if (!dicRS.ContainsKey(permission.Name))
                {
                    dicRS.Add(permission.Name, new List<string>());
                }

                foreach (var permissionDetail in permission.ListPermissionDetail)
                {
                    if (permissionDetail.View != null && permissionDetail.View.Value)
                    {
                        dicRS[permission.Name].Add(nameof(permissionDetail.View));
                    }
                    if (permissionDetail.Create != null && permissionDetail.Create.Value)
                    {
                        dicRS[permission.Name].Add(nameof(permissionDetail.Create));
                    }
                    if (permissionDetail.Edit != null && permissionDetail.Edit.Value)
                    {
                        dicRS[permission.Name].Add(nameof(permissionDetail.Edit));
                    }
                    if (permissionDetail.Delete != null && permissionDetail.Delete.Value)
                    {
                        dicRS[permission.Name].Add(nameof(permissionDetail.Delete));
                    }
                    if (permissionDetail.Request != null && permissionDetail.Request.Value)
                    {
                        dicRS[permission.Name].Add(nameof(permissionDetail.Request));
                    }
                    if (permissionDetail.Approve != null && permissionDetail.Approve.Value)
                    {
                        dicRS[permission.Name].Add(nameof(permissionDetail.Approve));
                    }
                }
            }
            foreach (var permissionGroup in listPermissionGroup)
            {
                foreach (var permission in permissionGroup.ListPermission)
                {
                    if (!dicRS.ContainsKey(permission.Name))
                    {
                        dicRS.Add(permission.Name, new List<string>());
                    }

                    foreach (var permissionDetail in permission.ListPermissionDetail)
                    {
                        if (permissionDetail.View != null && permissionDetail.View.Value)
                        {
                            dicRS[permission.Name].Add(nameof(permissionDetail.View));
                        }
                        if (permissionDetail.Create != null && permissionDetail.Create.Value)
                        {
                            dicRS[permission.Name].Add(nameof(permissionDetail.Create));
                        }
                        if (permissionDetail.Edit != null && permissionDetail.Edit.Value)
                        {
                            dicRS[permission.Name].Add(nameof(permissionDetail.Edit));
                        }
                        if (permissionDetail.Delete != null && permissionDetail.Delete.Value)
                        {
                            dicRS[permission.Name].Add(nameof(permissionDetail.Delete));
                        }
                        if (permissionDetail.Request != null && permissionDetail.Request.Value)
                        {
                            dicRS[permission.Name].Add(nameof(permissionDetail.Request));
                        }
                        if (permissionDetail.Approve != null && permissionDetail.Approve.Value)
                        {
                            dicRS[permission.Name].Add(nameof(permissionDetail.Approve));
                        }
                    }
                }
            }
            foreach (var keyVal in dicRS)
            {
                dicRS[keyVal.Key] = ConvertUtil.RemoveDuplicate(keyVal.Value);
            }
            return dicRS;
        }
    }
}
