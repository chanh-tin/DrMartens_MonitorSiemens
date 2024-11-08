using Microsoft.AspNetCore.Mvc;
using SourceBaseBE.MainService.Services;
using Microsoft.Extensions.Logging;
using iSoft.AspNetCore.Services;
using iSoft.AspNetCore.CommonFunc;
using iSoft.Common.Models.RequestModels;
using iSoft.Common.Models.ResponseModel;
using iSoft.Common;
using System;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using SourceBaseBE.Database.Models.RequestModels;
using SourceBaseBE.Database.Entities;
using SourceBaseBE.Database.Models.ResponseModels;
using iSoft.AspNetCore;

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/Example001")]
    public class Example001Controller : BaseExample001Controller
    {
        protected Example001Service _serviceImp;

        public Example001Controller(Example001Service service, ILogger<Example001Controller> logger, LanguageSystemService languageService)
          : base(service, logger, languageService)
        {
            _serviceImp = service;
        }

        //* multiLanguage flag
        public override bool IsMultiLanguage
        {
            get { return false; }
        }

        [HttpGet]
        [Route("get-list-filter-multi-lang")]
        public virtual IActionResult GetListFilterMultiLang([FromQuery] PagingFilterRequestModel request)
        {
            string funcName = nameof(GetListFilterMultiLang);

            try
            {
                this._logger.LogMsg(Messages.IFuncStart_0, funcName);

                request.Lang ??= ConstMain.ConstDefaultLang;

                PagingWithColumnsResponseModel result = new PagingWithColumnsResponseModel();

                var currentUserId = CommonFunction.GetCurrentUserId(this.HttpContext);

                result = _serviceImp.GetListFilterMultiLang(request);

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

        [HttpPost]
        //[UserPermission]
        [Route("upsert-multi-lang")]
        [RequestSizeLimit(1024 * 1000 * 500)] // 500MB
        public virtual async Task<IActionResult> UpsertMultiLang([FromForm] Example001MultiLangRequestModel requestModel, [FromQuery] string lang = null)
        {
            string funcName = nameof(UpsertMultiLang);

            try
            {
                _logger.LogMsg(Messages.IFuncStart_0, funcName);

                requestModel.DecryptData();

                lang ??= ConstMain.ConstDefaultLang;

                // TODO: Bị mất ID sau khi update data
                var currentUserId = CommonFunction.GetCurrentUserId(HttpContext);

                Example001Entity entity = null;
                if (requestModel.Id != null && requestModel.Id > 0)
                {
                    entity = _serviceCRUD.GetById((long)requestModel.Id, true);
                    if (entity == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    entity = new Example001Entity();
                }
                entity = requestModel.GetEntity(entity);

                var dicFormFile = requestModel.GetFiles();
                if (dicFormFile != null && dicFormFile.Count >= 1)
                {
                    Dictionary<string, string> dicImagePath = UploadUtil.UploadFile(dicFormFile);
                    entity = _serviceCRUD.SetFileURL(entity, dicImagePath);
                }

                var entity2 = _serviceCRUD.Upsert(entity, currentUserId);

                _logger.LogMsg(Messages.ISuccess_0_1, funcName, entity2.ToJson());
                return this.ResponseJSonObj(new Example001MultiLangResponseModel(lang).SetData(entity2));
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters(ex);
                _logger.LogMsg(errMessage);
                return this.ResponseError(errMessage);
            }
        }
    }
}