using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Http
{
    /// <summary>
    /// <see cref="IHttpExceptionHandler.HandleExceptionAsync"/>的参数
    /// </summary>
    public class HandleExceptionArgs
    {
        /// <summary>
        /// 获取或设置发送请求
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// 获取或设置发送请求时发生的异常
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 获取和设置当前失败的次数
        /// </summary>
        public int CurrentFailedTry { get; set; }


        /// <summary>
        /// 获取或设置发送请求的总次数
        /// </summary>
        public int TotalTries { get; set; }

        /// <summary>
        /// 获取一个指示，如果为<c>true</c>则发生重试
        /// </summary>
        public bool SupportsRetry
        {
            get { return TotalTries - CurrentFailedTry > 0; }
        }

        /// <summary>
        /// 获取或设置取消请求的标记
        /// </summary>
        public CancellationToken CancellationToken { get; set; }
    }

    /// <summary>
    /// 当异常处理程序被调用时，将会抛出一个异常HTTP请求
    /// </summary>
    public interface IHttpExceptionHandler
    {
        /// <summary>
        /// 处理发送一个HTTP请求时抛出的异常
        /// 必须遵守一个简单的规则，如果修改请求对象的方式，除了可以解决的，你必须返回<c>true</c>
        /// </summary>
        /// <param name="args">处理异常的参数，属性，如：请求/重试等</param>
        /// <returns>处理程序是否对于重新发送请求做出改变</returns>
        Task<bool> HandleExceptionAsync(HandleExceptionArgs args);
    }
}
