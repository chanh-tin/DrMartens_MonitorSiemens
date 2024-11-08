using iSoft.Common;
using iSoft.Common.Exceptions;
using iSoft.Common.Models.RequestModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using iSoft.Redis.Services;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using iSoft.Database.Models;
using UserEntity = SourceBaseBE.Database.Entities.UserEntity;
using ISoftProjectEntity = SourceBaseBE.Database.Entities.ISoftProjectEntity;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Common.Utils;


namespace SourceBaseBE.Database.Repository
{
    public class UserRepository : BaseUserRepository
    {
        public UserRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// GetById (@GenCRUD)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public UserEntity? GetProfile(long id, bool isTracking = false)
        {
            try
            {
                UserEntity? result = null;
                var dataSet = _context.Set<UserEntity>();
                IQueryable<UserEntity> queryable;
                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                queryable = queryable
                      .Include(entity => entity.ItemEmployee)
                      .Include(entity => entity.ListISoftProject)
                      .Include(entity => entity.ListPermission)
                      .Include(entity => entity.ListUserGroup)

                      .Where(entity => entity.DeletedFlag != true && entity.Id == id);

                if (!isTracking)
                {
                    queryable = queryable.Select(entity => new UserEntity()
                    {
                        Id = entity.Id,
                        Order = entity.Order,
                        DeletedFlag = entity.DeletedFlag,
                        CreatedAt = entity.CreatedAt,
                        CreatedBy = entity.CreatedBy,
                        UpdatedAt = entity.UpdatedAt,
                        UpdatedBy = entity.UpdatedBy,
                        Username = entity.Username,
                        Password = entity.Password,
                        DisplayName = entity.DisplayName,
                        FirstName = entity.FirstName,
                        MiddleName = entity.MiddleName,
                        LastName = entity.LastName,
                        Gender = entity.Gender,
                        PhoneNumber = entity.PhoneNumber,
                        Email = entity.Email,
                        Address = entity.Address,
                        Birthday = entity.Birthday,
                        CompanyName = entity.CompanyName,
                        Avatar = entity.Avatar,
                        Role = entity.Role,
                        License = entity.License,
                        EnableFlag = entity.EnableFlag,
                        LastLogin = entity.LastLogin,
                        Notes = entity.Notes,
                        EmployeeId = entity.EmployeeId,
                        ItemEmployee = entity.ItemEmployee,
                        ListISoftProject = entity.ListISoftProject == null ? null : entity.ListISoftProject.Where(x => x.DeletedFlag != true).ToList(),
                        ListPermission = entity.ListPermission == null ? null : entity.ListPermission.Where(x => x.DeletedFlag != true)
                            .Select(entity => new PermissionEntity()
                            {
                                Id = entity.Id,
                                Name = entity.Name,
                                EnabledFlag = entity.EnabledFlag,
                                ListPermissionDetail = entity.ListPermissionDetail == null ? null : entity.ListPermissionDetail.Where(x => x.DeletedFlag != true)
                                    .Select(entity => new PermissionDetailEntity()
                                    {
                                        Id = entity.Id,
                                        PermissionTable = entity.PermissionTable,
                                        View = entity.View,
                                        Create = entity.Create,
                                        Edit = entity.Edit,
                                        Delete = entity.Delete,
                                        Approve = entity.Approve,
                                    })
                                    .ToList(),
                            })
                            .ToList(),
                        ListUserGroup = entity.ListUserGroup == null ? null : entity.ListUserGroup.Where(x => x.DeletedFlag != true)
                            .Select(entity => new UserGroupEntity()
                            {
                                ListPermission = entity.ListPermission == null ? null : entity.ListPermission.Where(entity => entity.DeletedFlag != true)
                                    .Select(entity => new PermissionEntity()
                                    {
                                        Id = entity.Id,
                                        Name = entity.Name,
                                        EnabledFlag = entity.EnabledFlag,
                                        ListPermissionDetail = entity.ListPermissionDetail == null ? null : entity.ListPermissionDetail.Where(x => x.DeletedFlag != true)
                                            .Select(entity => new PermissionDetailEntity()
                                            {
                                                Id = entity.Id,
                                                PermissionTable = entity.PermissionTable,
                                                View = entity.View,
                                                Create = entity.Create,
                                                Edit = entity.Edit,
                                                Delete = entity.Delete,
                                                Approve = entity.Approve,
                                            })
                                            .ToList(),
                                    })
                                    .ToList(),
                            })
                            .ToList(),

                    });
                    result = queryable.FirstOrDefault();
                }
                else
                {
                    var entity = queryable.FirstOrDefault();
                    if (entity != null)
                    {
                        entity.ListISoftProject = entity.ListISoftProject == null ? null : entity.ListISoftProject.Where(x => x.DeletedFlag != true).ToList();
                        entity.ListPermission = entity.ListPermission == null ? null : entity.ListPermission.Where(x => x.DeletedFlag != true).ToList();
                        entity.ListUserGroup = entity.ListUserGroup == null ? null : entity.ListUserGroup.Where(x => x.DeletedFlag != true).ToList();

                    };
                    result = entity;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
    }
}
