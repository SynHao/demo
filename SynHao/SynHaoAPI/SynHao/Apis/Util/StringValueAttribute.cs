using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Util
{
    /// <summary>
    /// 定义包含成员字符串的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field,AllowMultiple=false)]
    public class StringValueAttribute :Attribute
    {
        private readonly string text;

        /// <summary>
        /// 属于该成员的文本
        /// </summary>
        public string Text { get { return text; } }

        /// <summary>
        /// 创建具有指定文本的新字符串的属性
        /// </summary>
        /// <param name="text"></param>
        public StringValueAttribute(string text)
        {
            text.ThrowIfNull("text");
            this.text = text;
        }
    }
}
