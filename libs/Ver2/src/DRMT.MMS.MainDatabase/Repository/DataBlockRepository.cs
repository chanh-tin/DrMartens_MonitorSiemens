using SourceBaseBE.Database.DBContexts;


namespace SourceBaseBE.Database.Repository
{
  public class DataBlockRepository : BaseDataBlockRepository
  {
    public DataBlockRepository(CommonDBContext dbContext)
        : base(dbContext)
    {
    }
    public override string GetRepositoryName()
    {
      return nameof(DataBlockRepository);
    }
  }
}
