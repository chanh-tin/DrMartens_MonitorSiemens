using Microsoft.AspNetCore.Mvc;
using SourceBaseBE.MainService.Services;
using Microsoft.Extensions.Logging;
using iSoft.AspNetCore.Services;

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/Tag")]
    public class TagController : BaseTagController
    {
        protected TagService _serviceImp;

        public TagController(TagService service, ILogger<TagController> logger, LanguageSystemService languageService)
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