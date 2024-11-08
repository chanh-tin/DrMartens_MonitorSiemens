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
    public class BaseUserRequestModel : BaseCRUDRequestModel<UserEntity>
    {
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string? DisplayName { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? MiddleName { get; set; }
        public virtual string? LastName { get; set; }
        public virtual EnumGender? Gender { get; set; }
        public virtual string? PhoneNumber { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? Address { get; set; }

        DateTime? _birthday { get; set; }
        public virtual string? Birthday
        {
            get
            {
                if (_birthday.HasValue)
                {
                    return _birthday.Value.ToString(ConstDateTimeFormat.YYYYMMDD);
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        _birthday = DateTimeUtil.GetDateTimeFromString(value, ConstDateTimeFormat.YYYYMMDD);
                    }
                    catch
                    {
                        _birthday = null;
                    }
                }
            }
        }
        public virtual string? CompanyName { get; set; }
        public virtual IFormFile? Avatar { get; set; }
        public virtual long? LanguageId { get; set; }
        public virtual long? CountryId { get; set; }
        public virtual string Role { get; set; }
        public virtual string? License { get; set; }
        public virtual EnumEnableFlag? EnableFlag { get; set; }

        DateTime? _lastLogin { get; set; }
        public virtual string? LastLogin
        {
            get
            {
                if (_lastLogin.HasValue)
                {
                    return _lastLogin.Value.ToString(ConstDateTimeFormat.YYYYMMDD);
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        _lastLogin = DateTimeUtil.GetDateTimeFromString(value, ConstDateTimeFormat.YYYYMMDD);
                    }
                    catch
                    {
                        _lastLogin = null;
                    }
                }
            }
        }
        public virtual string? Notes { get; set; }
        public virtual long? EmployeeId { get; set; }
        public virtual List<long>? ListISoftProject { get; set; }
        public virtual List<long>? ListPermission { get; set; }
        public virtual List<long>? ListUserGroup { get; set; }
        
        public override UserEntity GetEntity(UserEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.Username != null) entity.Username = this.Username;
            if (!string.IsNullOrEmpty(this.Password))
            {
                entity.Password = EncodeUtil.EncodePassword(this.Password, this.Username);
            }
            if (this.DisplayName != null) entity.DisplayName = this.DisplayName;
            if (this.FirstName != null) entity.FirstName = this.FirstName;
            if (this.MiddleName != null) entity.MiddleName = this.MiddleName;
            if (this.LastName != null) entity.LastName = this.LastName;
            if (this.Gender != null) entity.Gender = this.Gender;
            if (this.PhoneNumber != null) entity.PhoneNumber = this.PhoneNumber;
            if (this.Email != null) entity.Email = this.Email;
            if (this.Address != null) entity.Address = this.Address;
            if (this._birthday != null) entity.Birthday = this._birthday;
            if (this.CompanyName != null) entity.CompanyName = this.CompanyName;
            if (this.LanguageId != null) entity.LanguageId = this.LanguageId;
            if (this.CountryId != null) entity.CountryId = this.CountryId;
            if (this.Role != null) entity.Role = this.Role;
            if (this.License != null) entity.License = this.License;
            if (this.EnableFlag != null) entity.EnableFlag = this.EnableFlag;
            if (this._lastLogin != null) entity.LastLogin = this._lastLogin;
            if (this.Notes != null) entity.Notes = this.Notes;
            if (this.ListISoftProject != null)
            {
                entity.ISoftProjectIds = this.ListISoftProject.Select(x => x).ToList();
            }
            if (this.ListPermission != null)
            {
                entity.PermissionIds = this.ListPermission.Select(x => x).ToList();
            }
            if (this.ListUserGroup != null)
            {
                entity.UserGroupIds = this.ListUserGroup.Select(x => x).ToList();
            }
        
            return entity;
        }

        public override Dictionary<string, (string, IFormFile)> GetFiles()
        {
            Dictionary<string, (string, IFormFile)> dicRS = new Dictionary<string, (string, IFormFile)>();
            if (this.Avatar != null)
            {
                dicRS.Add(nameof(Avatar), (Path.Combine(ConstFolderPath.Images, ConstFolderPath.Upload), this.Avatar));
            }
            
            return dicRS;
        }
    }
}