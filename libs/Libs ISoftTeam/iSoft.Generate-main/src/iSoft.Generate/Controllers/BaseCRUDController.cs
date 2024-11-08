using iSoft.Common;
using iSoft.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static iSoft.Common.Messages;
using System.Linq;
using System.Collections.Generic;
using SourceBaseBE.MainService.Services;
using iSoft.Common.Models.RequestModels;
using iSoft.Common.Models.ResponseModels;
using iSoft.DBLibrary.Entities;
using SourceBaseBE.Database.Models.RequestModels;
using iSoft.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using SourceBaseBE.Database.Entities;
using iSoft.Common.Utils;
using SourceBaseBE.Database.Models.ResponseModels;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Enums;
using iSoft.Common.CommonFunctionNS;
using Microsoft.Extensions.Logging;
using iSoft.Redis.Services;
using System.IO;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using elFinder.NetCore;

namespace SourceBaseBE.MainService.Controllers
{
	[ApiController]
	[Route("api/v1/BaseCRUD")]
	public class BaseCRUDController<TEntity, TReqModel, TResModel> : ControllerBase where TEntity : BaseCRUDEntity, new() where TReqModel : BaseCRUDRequestModel<TEntity>, new() where TResModel : BaseCRUDResponseModel<TEntity>, new()
	{
        public ILogger<BaseCRUDController<TEntity, TReqModel, TResModel>> _logger;
        protected BaseCRUDService<TEntity> _serviceCRUD;
		public BaseCRUDController(BaseCRUDService<TEntity> serviceCRUD, ILogger<BaseCRUDController<TEntity, TReqModel, TResModel>> logger)
		{
			_logger = logger;
			this._serviceCRUD = serviceCRUD;
        }

        [HttpGet]
		[Route("get-list")]
		public virtual async Task<IActionResult> GetList([FromQuery] PagingRequestModel request)
		{
			string funcName = nameof(GetList);

			try
			{
				this._logger.LogMsg(Messages.IFuncStart_0, funcName);

				PagingResponseModel rs = new PagingResponseModel();

				List<TEntity> listEntity = _serviceCRUD.GetList(request);

				if (listEntity == null)
				{
					return this.ResponseError(null);
				}

				long totalRecord = _serviceCRUD.GetTotalRecord();

				var listItemResponse = new TResModel().SetData(listEntity);

				rs.ListData = listItemResponse;
				rs.TotalRecord = totalRecord;

				this._logger.LogMsg(Messages.ISuccess_0_1, funcName, listEntity.Count);

				return this.ResponseJSonObj(rs);
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters(ex);
                this._logger.LogMsg(errMessage);
                return this.ResponseError(errMessage);
            }
        }

		[HttpGet]
		[Route("get-form-data")]
		public virtual async Task<IActionResult> GetCreateFormData([FromQuery] long? Id)
		{
			string funcName = nameof(GetCreateFormData);

			try
			{
				this._logger.LogMsg(Messages.IFuncStart_0, funcName);

				List<Dictionary<string, object>> formDataObj = null;
				if (Id == null)
				{
					formDataObj = _serviceCRUD.GetFormDataObjElement(new TEntity());
				}
				else
				{
					var entity = this._serviceCRUD.GetById((long)Id);
					if (entity == null)
					{
						return NotFound();
					}
					formDataObj = _serviceCRUD.GetFormDataObjElement(entity);
				}

				this._logger.LogMsg(Messages.ISuccess_0_1, funcName, formDataObj);
				return this.ResponseJSonObj(formDataObj);
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters(ex);
                this._logger.LogMsg(errMessage);
                return this.ResponseError(errMessage);
            }
        }

		[HttpGet]
		[Route("get-detail")]
		public virtual async Task<IActionResult> GetDetail([FromQuery] long? Id)
		{
			string funcName = nameof(GetDetail);

			try
			{
				this._logger.LogMsg(Messages.IFuncStart_0, funcName);
				TEntity entity = null;
				if (Id == null || Id <= 0)
				{
					return NotFound();
				}
				else
				{
					entity = this._serviceCRUD.GetById((long)Id, false);
					if (entity == null)
					{
						return NotFound();
					}
				}

				this._logger.LogMsg(Messages.ISuccess_0_1, funcName, entity);
				return this.ResponseJSonObj(new TResModel().SetData(entity));
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters(ex);
                this._logger.LogMsg(errMessage);
                return this.ResponseError(errMessage);
            }
        }

		[HttpPost]
		[Authorize]
		[Route("upsert")]
        [RequestSizeLimit(1024 * 1000 * 500)] // 500MB
        public virtual async Task<IActionResult> Upsert([FromForm] TReqModel model)
		{
			string funcName = nameof(Upsert);

			try
			{
				this._logger.LogMsg(Messages.IFuncStart_0, funcName);

				var currentUserId = CommonFunction.GetCurrentUserId(this.HttpContext);

				TEntity entity = null;
				if (model.Id != null)
				{
					entity = this._serviceCRUD.GetById((long)model.Id, true);
					if (entity == null)
					{
						return NotFound();
					}
				}
				else
				{
					entity = new TEntity();
				}
				entity = model.GetEntity(entity);

				var dicFormFile = model.GetFiles();
				if (dicFormFile != null && dicFormFile.Count >= 1)
				{
					Dictionary<string, string> dicImagePath = UploadUtil.UploadFile(dicFormFile);
                    entity = this._serviceCRUD.SetFileURL(entity, dicImagePath);
				}

                var entity2 = this._serviceCRUD.Upsert(entity, currentUserId);

                this._logger.LogMsg(Messages.ISuccess_0_1, funcName, entity2.ToJson());
				return this.ResponseJSonObj(new TResModel().SetData(entity2));
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters(ex);
                this._logger.LogMsg(errMessage);
                return this.ResponseError(errMessage);
            }
        }

		[HttpPost]
		[Authorize]
		[Route("delete")]
		public virtual async Task<IActionResult> Delete([FromQuery] long Id)
		{
			string funcName = nameof(Delete);

			try
			{
				this._logger.LogMsg(Messages.IFuncStart_0, funcName);

				var currentUserId = CommonFunction.GetCurrentUserId(this.HttpContext);

				var count = this._serviceCRUD.Delete(Id, currentUserId, true);

				if (count <= 0)
				{
					return this.NotFound();
				}
				//CachedFunc.ClearRedisAll();
				this._logger.LogMsg(Messages.ISuccess_0_1, funcName, Id);
				return this.ResponseJSonObj(count);
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters(ex);
                this._logger.LogMsg(errMessage);
                return this.ResponseError(errMessage);
            }
        }
		public IActionResult DownloadFile(string filePath, bool deleteAfterRead=false)
		{
			var fileName = Path.GetFileName(filePath);
			Response.Headers.Add("Content-Disposition", $"filename={fileName}");
			var fileData = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
			if (deleteAfterRead)
			{
				System.IO.File.Delete(filePath);
			}
			return File(fileData.ToArray(), "application/vnd.ms-excel.sheet.macroEnabled.12", fileName);
		}
	}

}