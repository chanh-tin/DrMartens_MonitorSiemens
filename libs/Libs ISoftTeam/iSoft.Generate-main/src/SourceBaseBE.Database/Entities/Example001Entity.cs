using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;
using MathNet.Numerics.Differentiation;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("Example001s")]
    public class Example001Entity : BaseCRUDEntity
    {
        [DisplayField]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
        public string Name { get; set; }


        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isReadonly: true, maxLen: 255)]
        public string NameReadonly { get; set; }


        [StringLength(50)]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, validationRules: new EnumValidationRules[] { EnumValidationRules.NoSpecial })]
        public string Username { get; set; }


        [StringLength(510)]
        [FormDataTypeAttributeText(EnumFormDataType.Textarea, maxLen: 510)]
        public string? Description { get; set; }


        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Label, maxLen: 255)]
        public string? Label1 { get; set; }


        [JsonIgnore]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Password, maxLen: 50)]
        public string? Password1 { get; set; }


        [JsonIgnore]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Password, maxLen: 50, isRequired: true, validationRules: new EnumValidationRules[] { EnumValidationRules.ConfirmPassword })]
        public string? Password2 { get; set; }


        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Email, maxLen: 255)]
        public string? Email1 { get; set; }


        [StringLength(25)]
        [FormDataTypeAttributeText(EnumFormDataType.PhoneNumber, placeholder: "Phone Number", maxLen: 25)]
        public string? PhoneNumber1 { get; set; }


        [FormDataTypeAttributeDatetime(EnumFormDataType.DateOnly, min: "2023-01-10", max: "2024-12-10", "2024-01-01")]
        public DateTime? StartDate { get; set; }


        [FormDataTypeAttributeDatetime(EnumFormDataType.Datetime, min: "2023-01-10T23:58:00.000", max: "2024-12-10T23:58:00.000", "2024-01-01T23:58:00.000")]
        public DateTime? StartDateTime { get; set; }


        [FormDataTypeAttributeDatetime(EnumFormDataType.TimeOnly, min: "03:58:00", max: "23:58:00", "13:58:00")]
        public DateTime? TimeOnlyData { get; set; }


        [FormDataTypeAttributeNumber(EnumFormDataType.Timespan)]
        public int? RefreshTime1 { get; set; }


        [FormDataTypeAttributeNumber(EnumFormDataType.Timespan, minLevelDateTime: EnumLevelDatetime.Seconds, maxLevelDateTime: EnumLevelDatetime.Days, defaultVal: 3600)]
        public int? RefreshTime2 { get; set; }


        [FormDataTypeAttributeNumber(EnumFormDataType.IntegerNumber, min: 1, max: 100, defaultVal: 5, unit: "Seconds")]
        public int? RefreshTime3 { get; set; }


        [FormDataTypeAttributeSelect(EnumFormDataType.Select, new object[] { 1, 5, 10, 20 }, defaultVal: 5, unit: "Seconds")]
        public int? RefreshTime4 { get; set; }


        [StringLength(255)]
        [FormDataTypeAttributeSelect(EnumFormDataType.SelectMulti, new object[] { "A", "B", "C", "D" }, defaultVal: new object[] { "B", "C" }, unit: "Seconds")]
        public string? RefreshTime5 { get; set; }


        [FormDataTypeAttributeNumber(EnumFormDataType.FloatNumber, min: 0, max: 100000000000000, defaultVal: 0, unit: "VND")]
        public double? Price { get; set; }


        [FormDataTypeAttributeSelect(EnumFormDataType.Radio, new object[] { (int)EnumGender.Male, (int)EnumGender.Female, (int)EnumGender.Other }, isRequired: true, defaultVal: (int)EnumGender.Male)]
        public int? Gender { get; set; }


        [FormDataTypeAttributeSelect(EnumFormDataType.Checkbox, null, true)]
        public bool? Enable { get; set; }


        [StringLength(255)]
        [FormDataTypeAttributeSelect(EnumFormDataType.CheckboxMulti, new object[] { "a", "b", "c" }, new object[] { "b", "c" }, unit: "unit")]
        public string? CheckBoxValues { get; set; }


        [StringLength(255)]
        [FormDataTypeAttributeImage(EnumFormDataType.Image, 100, 100)]
        public string? Avatar { get; set; }


        [FormDataTypeAttributeImage(EnumFormDataType.ListImage, 100, 100)]
        public string? ListImage1 { get; set; }


        [StringLength(255)]
        [FormDataTypeAttributeFile(EnumFormDataType.File, new string[] { "xlsx", "xlsm", "pdf" }, 500)]
        public string? File1 { get; set; }


        [FormDataTypeAttributeFile(EnumFormDataType.ListFile, new string[] { "xlsx", "xlsm", "pdf" }, 500)]
        public string? ListFile1 { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(Example002Entity))]
        public long? Example002Id { get; set; }
        public Example002Entity? ItemExample002 { get; set; }


        [NotMapped]
        public List<long>? Example003Ids { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(Example003Entity), nameof(Example003Ids), EnumAttributeRelationshipType.Many2Many)]
        //[NotFormData]
        public List<Example003Entity>? ListExample003 { get; set; } = new();
    }
}
