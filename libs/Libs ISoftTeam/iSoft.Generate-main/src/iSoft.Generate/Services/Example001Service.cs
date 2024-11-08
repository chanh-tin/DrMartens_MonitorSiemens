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
    public class Example001Service : BaseExample001Service
    {
        protected Example001Repository _repositoryImp;
        public Example001Service(CommonDBContext dbContext, ILogger<Example001Service> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _repositoryCRUD = new Example001Repository(_dbContext);
            _repositoryBase = (Example001Repository)(_repositoryCRUD);
            _repositoryImp = (Example001Repository)_repositoryBase;
            _userRepository = new UserRepository(_dbContext);
            this._example002Repository = new Example002Repository(this._dbContext);
            this._example003Repository = new Example003Repository(this._dbContext);
            
        }
    }
}