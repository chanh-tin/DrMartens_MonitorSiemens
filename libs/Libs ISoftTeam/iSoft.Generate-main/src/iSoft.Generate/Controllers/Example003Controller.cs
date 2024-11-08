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

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/Example003")]
    public class Example003Controller : BaseExample003Controller
    {
        protected Example003Service _serviceImp;
        public Example003Controller(Example003Service service, ILogger<Example003Controller> logger)
          : base(service, logger)
        {
            _serviceImp = service;
        }
    }
}