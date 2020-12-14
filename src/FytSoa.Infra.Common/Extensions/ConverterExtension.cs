using System;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace FytSoa.Infra.Common.Extensions
{
    public sealed class ConverterExtension : JsonConverter
    {
        /// <summary>
        /// 转换成字符串的类型
        /// </summary>
        private readonly ConverterExtensionShip _ship;

        /// <summary>
        /// 大数据json序列化重写实例化
        /// </summary>
        public ConverterExtension()
        {
            _ship = (ConverterExtensionShip)0xFF;
        }

        /// <summary>
        /// 大数据json序列化重写实例化
        /// </summary>
        /// <param name="ship">转换成字符串的类型</param>
        public ConverterExtension(ConverterExtensionShip ship)
        {
            _ship = ship;
        }

        public override bool CanConvert(Type objectType)
        {
            var typecode = Type.GetTypeCode(objectType.Name.Equals("Nullable`1") ? objectType.GetGenericArguments().First() : objectType);
            switch (typecode)
            {
                case TypeCode.Decimal:
                    return (_ship & ConverterExtensionShip.Decimal) == ConverterExtensionShip.Decimal;
                case TypeCode.Double:
                    return (_ship & ConverterExtensionShip.Double) == ConverterExtensionShip.Double;
                case TypeCode.Int64:
                    return (_ship & ConverterExtensionShip.Int64) == ConverterExtensionShip.Int64;
                case TypeCode.UInt64:
                    return (_ship & ConverterExtensionShip.UInt64) == ConverterExtensionShip.UInt64;
                case TypeCode.Single:
                    return (_ship & ConverterExtensionShip.Single) == ConverterExtensionShip.Single;
                default: return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return AsType(reader.Value.ToString(), objectType);
        }

        /// <summary>
        /// 字符串格式数据转其他类型数据
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="destinationType">目标格式</param>
        /// <returns>转换结果</returns>
        public static object AsType(string input, Type destinationType)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(destinationType);
                if (converter.CanConvertFrom(typeof(string)))
                {
                    return converter.ConvertFrom(null, null, input);
                }

                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(destinationType))
                {
                    return converter.ConvertTo(null, null, input, destinationType);
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var objectType = value.GetType();
                var typeCode = Type.GetTypeCode(objectType.Name.Equals("Nullable`1") ? objectType.GetGenericArguments().First() : objectType);
                switch (typeCode)
                {
                    case TypeCode.Decimal:
                        writer.WriteValue(((decimal)value).ToString("f6"));
                        break;
                    case TypeCode.Double:
                        writer.WriteValue(((double)value).ToString("f4"));
                        break;
                    case TypeCode.Single:
                        writer.WriteValue(((float)value).ToString("f2"));
                        break;
                    default:
                        writer.WriteValue(value.ToString());
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 转换成字符串的类型
    /// </summary>
    [Flags]
    public enum ConverterExtensionShip
    {
        /// <summary>
        /// 长整数
        /// </summary>
        Int64 = 1,

        /// <summary>
        /// 无符号长整数
        /// </summary>
        UInt64 = 2,

        /// <summary>
        /// 浮点数
        /// </summary>
        Single = 4,

        /// <summary>
        /// 双精度浮点数
        /// </summary>
        Double = 8,

        /// <summary>
        /// 大数字
        /// </summary>
        Decimal = 16
    }
}
