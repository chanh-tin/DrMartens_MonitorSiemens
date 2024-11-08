// -----------------------------------------------------------------------------
// This file was automatically generated.
// Please do not edit this file manually.
//
// Generated Date: 
//
// -----------------------------------------------------------------------------

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
using iSoft.Common.Models.RequestModels;
using iSoft.Common.Models.ResponseModels;
using MathNet.Numerics.Statistics.Mcmc;
using System.Collections.Generic;
using System.Threading.Tasks;
using iSoft.Common.CommonFunctionNS;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Models.ResponseModel;

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/BaseUser")]
    public class BaseUserController : BaseCRUDController<UserEntity, UserRequestModel, UserResponseModel>
    {
        protected BaseUserService _serviceBase;
        public BaseUserController(BaseUserService service, ILogger<BaseUserController> logger)
          : base(service, logger)
        {
            _serviceCRUD = service;
            _serviceBase = (BaseUserService)_serviceCRUD;
        }

        [HttpGet]
        [Route("get-list-filter")]
        public IActionResult GetLisFilter([FromQuery] PagingFilterRequestModel request)
        {
            string funcName = nameof(GetLisFilter);

            try
            {
                this._logger.LogMsg(Messages.IFuncStart_0, funcName);

                PagingWithColumnsResponseModel result = new PagingWithColumnsResponseModel();

                var currentUserId = CommonFunction.GetCurrentUserId(this.HttpContext);

                result = _serviceBase.GetListFilter(request);

                if (result == null)
                {
                    this._logger.LogMsg(Messages.ISuccess_0_1, funcName, result);
                    return this.ResponseError(null);
                }

                this._logger.LogMsg(Messages.ISuccess_0_1, funcName, result);
                return this.ResponseJSonObj(result);
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters(ex);
                this._logger.LogMsg(errMessage);
                return this.ResponseError(errMessage);
            }
        }
        [HttpGet]
        [Route("export-data")]
        public IActionResult ExportReport([FromQuery] PagingFilterRequestModel request)
        {
            string funcName = nameof(ExportReport);
            Messages.Message errMessage = null;

            try
            {
                this._logger.LogMsg(Messages.IFuncStart_0, funcName);

                var ret = _serviceBase.ExportData(request);
                this._logger.LogMsg(Messages.ISuccess_0_1, funcName, "Export successfully");

                return DownloadFile(ret, true);
            }
            catch (DBException ex)
            {
                errMessage = Messages.ErrDBException.SetParameters(ex);
            }
            catch (BaseException ex)
            {
                errMessage = Messages.ErrBaseException.SetParameters(ex);
            }
            catch (Exception ex)
            {
                errMessage = Messages.ErrException.SetParameters(ex);
            }
            this._logger.LogMsg(errMessage);
            return this.ResponseError(errMessage);
        }
    }
}