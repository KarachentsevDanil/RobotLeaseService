using System;

namespace RLS.WebApi.Helpers.AutoMapper
{
    public class EnumTypeConverter<TEnum> : StringToTypeConverter<TEnum>
        where TEnum : struct, Enum
    {
        protected override TEnum Convert(string source)
        {
            return Enum.TryParse(source, out TEnum convertedResult) ? convertedResult : default;
        }

        protected override TEnum NullableConvert(string source)
        {
            return Convert(source);
        }
    }
}