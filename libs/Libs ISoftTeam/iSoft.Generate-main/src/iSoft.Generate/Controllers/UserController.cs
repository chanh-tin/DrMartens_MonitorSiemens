using Microsoft.AspNetCore.Mvc;
using Serilog;
using iSoft.Common.Exceptions;
using iSoft.Common;
using System;
using SourceBaseBE.Database.Models.ResponseModels;
using SourceBaseBE.Database.Entities;
using SourceBaseBE.MainService.Services;
using Microsoft.Extensions.Logging;
using SourceBaseBE.Database.Models.RequestModels;
using iSoft.Common.CommonFunctionNS;
using SourceBaseBE.MainService.CustomAttributes;

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/User")]
    public class UserController : BaseUserController
    {
        protected UserService _serviceImp;
        public UserController(UserService service, ILogger<UserController> logger)
          : base(service, logger)
        {
            _serviceImp = service;
        }

        [UserPermission]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            string funcName = nameof(GetProfile);

            try
            {
                this._logger.LogMsg(Messages.IFuncStart_0, funcName);

                long? currentUserId = CommonFunction.GetCurrentUserId(this.HttpContext);
                if (currentUserId == null)
                {
                    return this.NotFound();
                }

                var entity = this._serviceImp.GetProfile(currentUserId.Value, false);
                UserResponseModel rs = (UserResponseModel)(new UserResponseModel().SetData(entity));
                rs.SetPermission(entity);

                this._logger.LogMsg(Messages.ISuccess_0_1, funcName, rs);
                return this.ResponseJSonObj(rs);
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters(ex);
                this._logger.LogMsg(errMessage);
                return this.ResponseError(errMessage);
            }
        }
    }
}