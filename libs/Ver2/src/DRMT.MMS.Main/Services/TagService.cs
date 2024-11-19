using Microsoft.Extensions.Logging;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Repository;

namespace SourceBaseBE.MainService.Services
{
  public class TagService : BaseTagService
  {
    protected TagRepository _repositoryImp;
    public TagService(
        CommonDBContext dbContext,
        ILogger<TagService> logger
        )
        : base(dbContext, logger)
    {
      _repositoryCRUD = new TagRepository(_dbContextImp);
      _repositoryBase = (TagRepository)(_repositoryCRUD);
      _repositoryImp = (TagRepository)_repositoryBase;
      this._dataBlockRepository = new DataBlockRepository(dbContext);

    }

    public override string GetServiceName()
    {
      return nameof(TagService);
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