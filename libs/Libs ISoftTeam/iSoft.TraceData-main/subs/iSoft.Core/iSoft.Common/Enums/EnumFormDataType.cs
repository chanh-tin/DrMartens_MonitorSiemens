namespace iSoft.Common.Enums
{
    public enum EnumFormDataType
    {
        Hidden,
        DateOnly,
        TimeOnly,
        Datetime,
        Timespan,
        Label,
        Textarea,
        Textbox,
        Password,
        Email,
        PhoneNumber,
        IntegerNumber,
        FloatNumber,
        Checkbox,
        CheckboxMulti,
        Radio,
        Select,
        SelectMulti,
        Image,
        ListImage,
        File,
        ListFile,
    }

    public static class EnumExtensions
    {
        public static string ToStringValue(this EnumFormDataType enumValue)
        {
            switch (enumValue)
            {
                //case EnumFormDataType.readonlyType:
                //    return "readonly";
                default:
                    return enumValue.ToString();
            }
        }
    }
}
