using iSoft.DBLibrary.DBConnections.Factory;
using iSoft.Common.Enums.DBProvider;
using Serilog;
using iSoft.Common.ConfigsNS;
using SourceBaseBE.Database.Repository;
using SourceBaseBE.Database.DBContexts;
using System;
using iSoft.Common.Exceptions;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using iSoft.Common.Models.RequestModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Database.Entities;
using System.Linq;
using iSoft.Database.Extensions;
using iSoft.Common.Models;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common;
using iSoft.Common.Enums;
using iSoft.Database.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using iSoft.Common.Utils;
using iSoft.Database.DBContexts;
using iSoft.RabbitMQ.Services;
using static iSoft.Common.Messages;
using iSoft.Common.Payloads;
using iSoft.AspNetCore.RabbitMQ;
using iSoft.AspNetCore.Enums;
using iSoft.Common.Models.ResponseModel;
using SourceBaseBE.Database.Models.ResponseModels;

namespace SourceBaseBE.MainService.Services
{
    public class Example001Service : BaseExample001Service
    {
        protected Example001Repository _repositoryImp;
        public Example001Service(
            CommonDBContext dbContext,
            ILogger<Example001Service> logger
            )
            : base(dbContext, logger)
        {
            _repositoryCRUD = new Example001Repository(_dbContextImp);
            _repositoryBase = (Example001Repository)(_repositoryCRUD);
            _repositoryImp = (Example001Repository)_repositoryBase;
            this._example002Repository = new Example002Repository(dbContext);
            this._example003Repository = new Example003Repository(dbContext);
            
        }

        public override string GetServiceName()
        {
            return nameof(Example001Service);
        }

        //* SyncServiceTransit flag
        public override bool IsSyncServiceTransit
        {
            get { return false; }
        }

        //* multiLanguage flag
        public override bool IsMultiLanguage
        {
            get { return false; }
        }
        public virtual PagingWithColumnsResponseModel GetListFilterMultiLang(
            PagingFilterRequestModel request,
            bool isDirect = false,
            bool isTracking = false)
        {
            var (list, totalRecord) = _repositoryImp.GetListFilterMultiLang(request, isDirect, isTracking);

            PagingWithColumnsResponseModel rs = new PagingWithColumnsResponseModel();
            List<Example001MultiLangResponseModel> listItemResponse = 
                new Example001MultiLangResponseModel(request.Lang).SetData(list).Cast<Example001MultiLangResponseModel>().ToList();

            //* get column language
            var columns = new Example001ResponseModel().GetColumnAttribute();

            this.TranslateData(columns, request.Lang, nameof(Example001Entity));

            rs.ListData = listItemResponse.Cast<object>().ToList();
            rs.TotalRecord = totalRecord;
            rs.Columns = columns;

            return rs;
        }
    }
}