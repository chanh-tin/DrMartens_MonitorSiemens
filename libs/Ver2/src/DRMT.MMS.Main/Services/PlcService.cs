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

namespace SourceBaseBE.MainService.Services
{
    public class PlcService : BasePlcService
    {
        protected PlcRepository _repositoryImp;
        public PlcService(
            CommonDBContext dbContext,
            ILogger<PlcService> logger
            )
            : base(dbContext, logger)
        {
            _repositoryCRUD = new PlcRepository(_dbContextImp);
            _repositoryBase = (PlcRepository)(_repositoryCRUD);
            _repositoryImp = (PlcRepository)_repositoryBase;
            this._dataBlockRepository = new DataBlockRepository(dbContext);
            
        }

        public override string GetServiceName()
        {
            return nameof(PlcService);
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
    }
}