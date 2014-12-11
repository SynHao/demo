using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Http
{

    /// <summary>参数类<see cref="IHttpUnsuccessfulResponseHandler.HandleResponseAsync"/></summary>
    public class HandleUnsuccessfulResponseArgs
    {
        /// <summary>
        /// 获取或设置发送请求
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// 获取或设置异常反应
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// 获取或设置尝试发送请求的次数
        /// </summary>
        public int TotalTries { get; set; }

        /// <summary>
        /// 获取或设置当前失败的尝试次数
        /// </summary>
        public int CurrentFailedTry { get; set; }

        /// <summary>
        /// 获取一个指示，如果返回<c>true</c>者会进行重试
        /// </summary>
        public bool SupportRetry
        {
            get { return TotalTries - CurrentFailedTry > 0; }
        }

        /// <summary>
        /// 获取或设置请求的取消的标记
        /// </summary>
        public CancellationToken CancellationToken { get; set; }
    }


    /// <summary>
    /// 当发送一个HTTP请求时异常的HTTP响应返回其调用失败响应处理。
    /// </summary>
    public interface IHttpUnSuccessfulResponseHandler
    {
        /// <summary>
        /// 发送一个HTTP请求时处理的异常反应。
        /// 一个简单的规则必须遵守，如果修改请求对象的方式，异常反应就可以解决，你必须返回<c>true</c>
        /// </summary>
        /// <param name="args">
        /// 处理响应参数，它包含如请求，响应当前失败的尝试的特性。
        /// </param>
        /// <returns>这是否处理程序作出了要求重新发送请求的变化。</returns>
        Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args);
    }
}
