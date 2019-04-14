using AutoMapper;
using System;
using System.Globalization;
using Converter = System.Convert;

namespace RLS.WebApi.Helpers.AutoMapper
{
    public class StringToTypeConverter<T> : ITypeConverter<string, T>
    {
        public StringToTypeConverter()
        {
            Type = typeof(T);
            BaseNullableType = Nullable.GetUnderlyingType(Type);
            IsNullable = BaseNullableType != null;
        }

        protected Type Type { get; set; }

        protected bool IsNullable { get; set; }

        protected Type BaseNullableType { get; set; }

        public T Convert(string source, T destination, ResolutionContext context)
        {
            if (IsNullable)
            {
                if (string.IsNullOrEmpty(source))
                {
                    return default;
                }
                else
                {
                    return NullableConvert(source);
                }
            }
            else
            {
                return Convert(source);
            }
        }

        protected virtual T Convert(string source)
        {
            return (T)Converter.ChangeType(source, Type, CultureInfo.InvariantCulture);
        }

        protected virtual T NullableConvert(string source)
        {
            return (T)Converter.ChangeType(source, BaseNullableType, CultureInfo.InvariantCulture);
        }
    }
}