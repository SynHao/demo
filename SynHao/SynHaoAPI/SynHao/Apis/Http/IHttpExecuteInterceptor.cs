using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Http
{
    /// <summary>
    /// HTTP请求执行拦截器拦截<see cref="System.Net.Http.HttpRequestMessage"/>它已发送之前。示例用法是附加“授权”标头的请求。
    /// </summary>
    public interface IHttpExecuteInterceptor
    {
        /// <summary>
        /// 该请求被发送之前调用
        /// </summary>
        /// <param name="request">HTTP请求信息</param>
        /// <param name="cancellationToke">取消标记，取消操作</param>
        Task InterceptAsync(HttpRequestMessage request, CancellationToken cancellationToke);
    }
}
