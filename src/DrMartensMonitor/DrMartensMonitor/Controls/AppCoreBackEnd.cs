using DrMartensMonitor.Services;

namespace DrMartensMonitor.Controls
{
  public partial class AppCore
  {
    private static AppCore _ins = new AppCore();
    public static AppCore Ins
    {
      get
      {
        return _ins == null ? _ins = new AppCore() : _ins;
      }
    }

    private DBProcessingService _dbProcessingService = new DBProcessingService();
    public void Init()
    {
      StartShowUI();

      _dbProcessingService.ConnectDB();
    }
  }
}
