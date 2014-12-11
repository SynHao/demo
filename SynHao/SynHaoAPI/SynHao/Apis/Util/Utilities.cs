using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Util
{
    /// <summary>
    /// 其中包含辅助方法与扩展方法的实用程序类
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// 返回库的版本
        /// </summary>
        /// <returns></returns>
        internal static string GetLibraryVersion()
        {
            return Regex.Match(typeof(Utilities).Assembly.FullName, "Version=([\\d\\.]+)").Groups[1].ToString();
        }

        /// <summary>
        /// 如果对象为空的话，抛出一个<see cref="System.ArgumentNullException"/>
        /// </summary>
        internal static T ThrowIfNull<T>(this T obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return obj;
        }

        /// <summary>
        /// 如果字符串为<c>null</c>或者空，则抛出<see cref="System.ArgumentNullException"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        internal static string ThrowIfNullOrEmpty(this string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("参数为空", paramName);
            }

            return str;
        }

        /// <summary>
        /// 枚举的内容为空或<c>null</c>的话，返回<c>true</c>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <returns></returns>
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> coll)
        {
            return coll == null || coll.Count() == 0;
        }

        /// <summary>
        /// 返回特殊成员的第一个匹配属性或者<c>null</c>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static T GetCustomAttribute<T>(this MemberInfo info) where T : Attribute
        {
            object[] results = info.GetCustomAttributes(typeof(T), false);
            return results.Length == 0 ? null : (T)results[0];
        }

        /// <summary>
        /// 返回枚举的定义字符串值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string GetStringValue(this Enum value)
        {
            FieldInfo entry = value.GetType().GetField(value.ToString());
            entry.ThrowIfNull("value");

            // 如果设置，返回值
            var attribute = entry.GetCustomAttribute<StringValueAttribute>();

            if (attribute!=null)
            {
                return attribute.Text;
            }

            // 否则，抛出一个异常
            throw new ArgumentException(
                string.Format("枚举值{0}不包含在一个字符串值属性中", entry), "value");
        }

        /// <summary>
        /// 尝试将指定对象转换为字符串。
        /// 如果有使用自定义类型转换器。
        /// 为空对象返回空
        /// </summary>
        internal static string ConvertToString(object o)
        {
            if (o == null)
            {
                return null;
            }

            if(o.GetType().IsEnum)
            {
                // 尝试转换使用StringValue属性的枚举值
                var enumType = o.GetType();
                FieldInfo field = enumType.GetField(o.ToString());
                StringValueAttribute attribute = field.GetCustomAttribute<StringValueAttribute>();
                return attribute != null ? attribute.Text : o.ToString();
            }

            if (o is DateTime)
            {
                // Honor RFC3339
                return ConvertToRFC3339((DateTime)o);
            }

            return o.ToString();
        }

        /// <summary>
        /// 输入一个日期字符串，RFC3339（http://www.ietf.org/rfc/rfc3339.txt）转换
        /// </summary>
        internal static string ConvertToRFC3339(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                date = date.ToUniversalTime();
            }
            return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 如果输入的字符串为日期的有效表示方法，则解析输入的字符串，并返回<see cref="System.DateTime"/>
        /// 否则返回<c>null</c>
        /// </summary>
        public static DateTime? GetDateTimeFromString(string raw)
        {
            DateTime result;
            if (!DateTime.TryParse(raw, out result))
            {
                return null;
            }
            return result;
        }

        /// <summary>
        /// 返回一个字符串（由RFC3339）形成<see cref="DateTime"/>的实例
        /// </summary>
        public static string GetStringFromDateTime(DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }
            return ConvertToRFC3339(date.Value);
        }
    }
}
