using Newtonsoft.Json;

namespace iSoft.Common.Models.ResponseModels
{
	public class ColumnResponseModel
    {
        public string? Key { get; set; }
        public string? DisplayName { get; set; }
        public bool? Searchable { get; set; }
        public bool? Filterable { get; set; }
        public bool? Displayable { get; set; }

        public ColumnResponseModel()
		{
			this.Filterable = false;
			this.Searchable = false;
            this.Displayable = true;
		}

        public ColumnResponseModel(string? key, string? displayName, bool? searchable = false, bool? filterable = false, bool? displayable = false)
        {
            Key = key;
            DisplayName = displayName;
            Searchable = searchable;
            Filterable = filterable;
            Displayable = displayable;
        }
	}
}
