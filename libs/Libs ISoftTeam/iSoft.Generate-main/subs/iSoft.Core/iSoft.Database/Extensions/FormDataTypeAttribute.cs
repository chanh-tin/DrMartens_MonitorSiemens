using iSoft.Common;
using iSoft.Common.Enums;
using iSoft.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Common.ConstCommon;

namespace iSoft.Database.Extensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormDataTypeAttribute : Attribute
    {
        //public string? RefField { get; set; } = null;
        public EnumFormDataType TypeName { get; set; }
        public bool IsRequired { get; set; }
        public int? Width { get; set; } = null;
        public int? Height { get; set; } = null;
        public string? Title { get; set; } = null;
        //public string? ImageQuality { get; set; } = null;
        public object? DefaultVal { get; set; } = null;
        public object? Min { get; set; } = null;
        public object? Max { get; set; } = null;
        public int? MaxLen { get; set; } = null;
        public string? Unit { get; set; } = null;
        public string? Placeholder { get; set; } = null;
        public List<object>? Options { get; set; } = new List<object>();
        public List<Dictionary<string, string>>? ValidationRules { get; set; } = null;
        public bool IsReadonly { get; set; } = false;
        public EnumLevelDatetime? MinLevelDateTime { get; set; } = null;
        public EnumLevelDatetime? MaxLevelDateTime { get; set; } = null;
        public bool? Searchable { get; set; } = true;
        public int? MaxRow { get; set; } = null;
        public string[]? FileExtension { get; set; } = null;
        public int? MaxFileSizeInMB { get; set; } = 10;

        public FormDataTypeAttribute() {
        }

        internal void PrepareData()
        {
            if (this.TypeName == EnumFormDataType.DateOnly)
            {
                this.Min = this.Min?.ToString().Replace("{CURRENT}", DateTime.Today.ToString(ConstDateTimeFormat.YYYYMMDD));
                this.Max = this.Max?.ToString().Replace("{CURRENT}", DateTime.Today.ToString(ConstDateTimeFormat.YYYYMMDD));
                this.DefaultVal = this.DefaultVal?.ToString().Replace("{CURRENT}", DateTime.Today.ToString(ConstDateTimeFormat.YYYYMMDD));
            }
            else if (this.TypeName == EnumFormDataType.Datetime)
            {
                this.Min = this.Min?.ToString().Replace("{CURRENT}", DateTime.Now.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF));
                this.Max = this.Max?.ToString().Replace("{CURRENT}", DateTime.Now.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF));
                this.DefaultVal = this.DefaultVal?.ToString().Replace("{CURRENT}", DateTime.Now.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF));
            }
        }
    }
}
