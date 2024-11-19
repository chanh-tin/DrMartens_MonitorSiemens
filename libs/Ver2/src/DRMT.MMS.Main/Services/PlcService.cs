using Microsoft.Extensions.Logging;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Repository;

namespace SourceBaseBE.MainService.Services
{
  public class PlcService : BasePlcService
  {
    protected PlcRepository _repositoryImp;
    public PlcService(
        CommonDBContext dbContext,
        ILogger<PlcService> logger
        )
        : base(dbContext, logger)
    {
      _repositoryCRUD = new PlcRepository(_dbContextImp);
      _repositoryBase = (PlcRepository)(_repositoryCRUD);
      _repositoryImp = (PlcRepository)_repositoryBase;
      this._dataBlockRepository = new DataBlockRepository(dbContext);

    }

    public override string GetServiceName()
    {
      return nameof(PlcService);
    }

    //* SyncServiceTransit flag
    public override bool IsSyncServiceTransit
    {
      get { return false; }
    }

    //* multiLanguage flag
    public override bool IsMultiLanguage
    {
      get { return false; }
    }
  }
}