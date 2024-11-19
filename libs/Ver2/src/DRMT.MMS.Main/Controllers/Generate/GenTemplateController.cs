using Microsoft.AspNetCore.Mvc;
using SourceBaseBE.MainService.Services;
using Microsoft.Extensions.Logging;
using iSoft.AspNetCore.Services;

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/GenTemplate")]
    public class GenTemplateController : BaseGenTemplateController
    {
        protected GenTemplateService _serviceImp;

        public GenTemplateController(GenTemplateService service, ILogger<GenTemplateController> logger, LanguageSystemService languageService)
          : base(service, logger, languageService)
        {
            _serviceImp = service;
        }

        //* multiLanguage flag
        public override bool IsMultiLanguage
        {
            get { return false; }
        }
    }
}