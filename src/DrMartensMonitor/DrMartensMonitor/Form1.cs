namespace DrMartensMonitor
{
  public partial class Form1 : Form
  {

    public Form1()
    {
      InitializeComponent();
    }

    #region Singleton parttern
    private static Form1 _Instance;

    public static Form1 Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new Form1();
        }
        return _Instance;
      }
    }
    #endregion
  }
}
