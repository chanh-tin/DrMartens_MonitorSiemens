using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class MasterOeeManagementtReponseModel
    {

        [JsonProperty("Workshop", NullValueHandling = NullValueHandling.Ignore)]
        public List<MasterDataResponseModel>? WorkShops { get; set; }

        [JsonProperty("Line", NullValueHandling = NullValueHandling.Ignore)]
        public List<MasterDataResponseModel>? Lines { get; set; }

        [JsonProperty("OeePoint", NullValueHandling = NullValueHandling.Ignore)]
        public List<MasterDataResponseModel>? OeePoints { get; set; }

        [JsonProperty("LossName", NullValueHandling = NullValueHandling.Ignore)]
        public List<MasterDataResponseModel>? LossNames { get; set; }

        [JsonProperty("LossPosition", NullValueHandling = NullValueHandling.Ignore)]
        public List<MasterDataResponseModel>? LossPositions { get; set; }

        [JsonProperty("LossGroup", NullValueHandling = NullValueHandling.Ignore)]
        public List<MasterDataResponseModel>? LossGroups { get; set; }

        [JsonProperty("LossDescription", NullValueHandling = NullValueHandling.Ignore)]
        public List<MasterDataResponseModel>? LossDescriptions { get; set; }


    }
}
