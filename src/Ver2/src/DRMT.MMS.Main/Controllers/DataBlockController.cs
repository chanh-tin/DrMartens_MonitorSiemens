using Microsoft.AspNetCore.Mvc;
using SourceBaseBE.MainService.Services;
using Microsoft.Extensions.Logging;
using iSoft.AspNetCore.Services;

namespace SourceBaseBE.MainService.Controllers
{
    [ApiController]
    [Route("api/v1/DataBlock")]
    public class DataBlockController : BaseDataBlockController
    {
        protected DataBlockService _serviceImp;

        public DataBlockController(DataBlockService service, ILogger<DataBlockController> logger, LanguageSystemService languageService)
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