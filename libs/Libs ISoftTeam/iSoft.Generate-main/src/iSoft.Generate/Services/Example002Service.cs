using iSoft.DBLibrary.DBConnections.Factory;
using iSoft.Common.Enums.DBProvider;
using Serilog;
using iSoft.Common.ConfigsNS;
using SourceBaseBE.Database.Repository;
using SourceBaseBE.Database.DBContexts;
using MathNet.Numerics.Statistics.Mcmc;
using System;
using iSoft.Common.Exceptions;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using iSoft.Common.Models.RequestModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Database.Entities;
using System.Linq;
using SourceBaseBE.MainService.Models;
using SourceBaseBE.Database.Enums;
using iSoft.Database.Extensions;
using iSoft.Common.Models;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common;
using iSoft.Common.Enums;
using iSoft.Database.Models;
using UserEntity = SourceBaseBE.Database.Entities.UserEntity;
using ISoftProjectEntity = SourceBaseBE.Database.Entities.ISoftProjectEntity;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using iSoft.Common.Utils;

namespace SourceBaseBE.MainService.Services
{
    public class Example002Service : BaseExample002Service
    {
        protected Example002Repository _repositoryImp;
        public Example002Service(CommonDBContext dbContext, ILogger<Example002Service> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _repositoryCRUD = new Example002Repository(_dbContext);
            _repositoryBase = (Example002Repository)(_repositoryCRUD);
            _repositoryImp = (Example002Repository)_repositoryBase;
            _userRepository = new UserRepository(_dbContext);
            this._example001Repository = new Example001Repository(this._dbContext);
            
        }
    }
}