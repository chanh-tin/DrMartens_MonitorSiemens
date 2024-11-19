using Microsoft.Extensions.Logging;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Repository;

namespace SourceBaseBE.MainService.Services
{
  public class DataBlockService : BaseDataBlockService
  {
    protected DataBlockRepository _repositoryImp;
    public DataBlockService(
        CommonDBContext dbContext,
        ILogger<DataBlockService> logger
        )
        : base(dbContext, logger)
    {
      _repositoryCRUD = new DataBlockRepository(_dbContextImp);
      _repositoryBase = (DataBlockRepository)(_repositoryCRUD);
      _repositoryImp = (DataBlockRepository)_repositoryBase;
      this._tagRepository = new TagRepository(dbContext);
      this._plcRepository = new PlcRepository(dbContext);

    }

    public override string GetServiceName()
    {
      return nameof(DataBlockService);
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