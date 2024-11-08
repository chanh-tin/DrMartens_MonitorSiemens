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
    public class UserService : BaseUserService
    {
        protected UserRepository _repositoryImp;
        public UserService(CommonDBContext dbContext, ILogger<UserService> logger)
          : base(dbContext, logger)
        {
            _repositoryBase = new UserRepository(_dbContext);
            _repositoryImp = (UserRepository)_repositoryBase;
        }
        public UserEntity GetProfile(long id, bool isTracking = false)
        {
            var entity = _repositoryImp.GetProfile(id, isTracking);
            var entityRS = (UserEntity)_userRepository.FillTrackingUser(entity);
            return entityRS;
        }
    }
}