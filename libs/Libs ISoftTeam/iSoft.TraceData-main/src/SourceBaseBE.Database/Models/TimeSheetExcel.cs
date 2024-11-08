using iSoft.Common.Utils;

using SourceBaseBE.Database.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SourceBaseBE.Database.Models
{
	public class TimeSheetExcel
	{
		public string MNV { get; set; }
		public string Name { get; set; }
		public string Date { get; set; }
		public string DOW { get; set; }
		public string TimeIn { get; set; }
		public string TimeOut { get; set; }
		public string Department { get; set; }
		public static List<TimeSheetExcel> SetDatas(IEnumerable<IGrouping<long?, TimeSheetEntity>> timeSheets)
		{
			var ret = new List<TimeSheetExcel>();
			foreach (var empTs in timeSheets)
			{
				var ins = empTs.GroupBy(x => DateTimeUtil.GetStartDate(x.RecordedTime.Value)).ToList();
				foreach (var item in ins)
				{
					var datas = item.Select(x => x).ToList();
					for (int i = 0; i < datas.Count - 1; i+=2)
					{
						ret.Add(new TimeSheetExcel()
						{
							MNV = datas[i].Employee?.EmployeeMachineCode,
							Date = datas[i].RecordedTime?.ToString("dd/MM/yyyy"),
							DOW = datas[i].RecordedTime?.DayOfWeek.ToString(),
							Name = datas[i].Employee?.Name,
							TimeIn = datas[i].RecordedTime?.ToString("HH:mm:ss"),
							TimeOut =i>datas.Count-1?"": datas[i+1].RecordedTime?.ToString("HH:mm:ss"),
							Department = datas[i].Employee?.Department?.Name
						});
					}
				}
			}

			return ret;
		}
	}
}
