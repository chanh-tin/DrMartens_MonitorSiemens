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
    [Route("api/v1/Example001")]
    public class Example001Controller : BaseExample001Controller
    {
        protected Example001Service _serviceImp;
        public Example001Controller(Example001Service service, ILogger<Example001Controller> logger)
          : base(service, logger)
        {
            _serviceImp = service;
        }
    }
}