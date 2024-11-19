using Microsoft.AspNetCore.Mvc;
using SourceBaseBE.MainService.Services;
using Microsoft.Extensions.Logging;
using iSoft.AspNetCore.Services;

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/Example002")]
    public class Example002Controller : BaseExample002Controller
    {
        protected Example002Service _serviceImp;

        public Example002Controller(Example002Service service, ILogger<Example002Controller> logger, LanguageSystemService languageService)
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