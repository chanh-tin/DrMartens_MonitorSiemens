using SourceBaseBE.Database.DBContexts;


namespace SourceBaseBE.Database.Repository
{
  public class PlcRepository : BasePlcRepository
  {
    public PlcRepository(CommonDBContext dbContext)
        : base(dbContext)
    {
    }
    public override string GetRepositoryName()
    {
      return nameof(PlcRepository);
    }
  }
}
