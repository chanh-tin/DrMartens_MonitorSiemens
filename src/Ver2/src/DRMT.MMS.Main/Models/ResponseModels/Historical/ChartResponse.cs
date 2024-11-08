using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using iSoft.Common.Models;
using iSoft.ElasticSearch.Models;

namespace SourceBaseBE.MainService.Models.ResponseModels.Historical
{
    public class ChartReponse
    {
        public string Interval { get; set; }
        public List<iSoft.ElasticSearch.Models.ChartDataModel> Data { get; set; }

        public override string ToString()
        {
            return $"{Interval}, {Data}";
        }
        public static ChartReponse Create(string interval, List<iSoft.ElasticSearch.Models.ChartDataModel> data)
        {
            return new ChartReponse()
            {
                Interval = interval,
                Data = data,
            };
        }
    }

}
