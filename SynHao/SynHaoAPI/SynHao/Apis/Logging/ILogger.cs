using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// 获取一个指示调试输出是否记录或没有。
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 返回与指定类型关联的记录
        /// </summary>
        /// <param name="type">本记录器所属类型</param>
        /// <returns>相关记录类型</returns>
        ILogger ForType(Type type);

        /// <summary>
        /// 返回与指定类型关联的记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>相关记录类型</returns>
        ILogger ForType<T>();

        /// <summary>
        /// 记录一条消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="formatArgs">String.Format参数</param>
        void Info(string message, params object[] formatArgs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="formatArgs"></param>
        void Warning(string message, params object[] formatArgs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="formatArgs"></param>
        void Debug(string message, params object[] formatArgs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="formatArgs"></param>
        void Error(Exception exception, string message, params object[] formatArgs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="formatArgs"></param>
        void Error(string message, params object[] formatArgs);
    }
}
