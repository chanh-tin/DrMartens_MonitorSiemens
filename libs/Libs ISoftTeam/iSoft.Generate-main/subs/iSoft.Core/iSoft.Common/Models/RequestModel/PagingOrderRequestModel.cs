

namespace iSoft.Common.Models.RequestModels
{
  public class PagingOrdersRequestModel : PagingRequestModel
  {
    public List<string>? Orders { get; set; }
    public List<int>? Sorts { get; set; }
  }
}