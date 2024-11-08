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
    [Route("api/v1/GenTemplate")]
    public class GenTemplateController : BaseGenTemplateController
    {
        protected GenTemplateService _serviceImp;
        public GenTemplateController(GenTemplateService service, ILogger<GenTemplateController> logger)
          : base(service, logger)
        {
            _serviceImp = service;
        }
    }
}