using iSoft.AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SourceBaseBE.MainService.Services;

namespace SourceBaseBE.MainService.Controllers
{
  [ApiController]
  [Route("api/v1/Plc")]
  public class PlcController : BasePlcController
  {
    protected PlcService _serviceImp;

    public PlcController(PlcService service, ILogger<PlcController> logger, LanguageSystemService languageService)
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