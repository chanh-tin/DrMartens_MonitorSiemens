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
    [Route("api/v1/Example002")]
    public class Example002Controller : BaseExample002Controller
    {
        protected Example002Service _serviceImp;
        public Example002Controller(Example002Service service, ILogger<Example002Controller> logger)
          : base(service, logger)
        {
            _serviceImp = service;
        }
    }
}