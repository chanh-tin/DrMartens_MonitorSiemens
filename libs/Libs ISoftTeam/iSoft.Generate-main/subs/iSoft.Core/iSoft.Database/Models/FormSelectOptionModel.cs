using Newtonsoft.Json;

namespace iSoft.Database.Models
{
	public class FormSelectOptionModel
	{
		[JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
		public object Id { get; set; }


		[JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
		public object DisplayName { get; set; }


		[JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
		public string Description { get; set; }

		public FormSelectOptionModel(long id, string displayName)
		{
			Id = id;
			DisplayName = displayName;
        }

        public FormSelectOptionModel(object id, object displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public FormSelectOptionModel(string id, string displayName)
		{
			Id = id;
			DisplayName = displayName;
		}
		public FormSelectOptionModel(long id, string displayName, string desc)
		{
			Id = id;
			DisplayName = displayName;
			this.Description = desc;
		}
	}
}
