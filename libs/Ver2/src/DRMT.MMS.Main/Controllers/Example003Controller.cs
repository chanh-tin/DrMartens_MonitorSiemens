using Microsoft.AspNetCore.Mvc;
using SourceBaseBE.MainService.Services;
using Microsoft.Extensions.Logging;
using iSoft.AspNetCore.Services;

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/Example003")]
    public class Example003Controller : BaseExample003Controller
    {
        protected Example003Service _serviceImp;

        public Example003Controller(Example003Service service, ILogger<Example003Controller> logger, LanguageSystemService languageService)
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