using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Http
{
    /// <summary>
    /// HTTP客户端初始化改变HTTP客户端的行为
    /// 使用此初始化修改默认值如超时，与重试次数
    /// 你还可以设置不同的处理方式如<see cref="IHttpUnsuccessfulResponseHandler"/>
    /// <see cref="IHttpExceptionHandler"/>和<see cref="IHttpExecuteInterceptor"/>
    /// </summary>
    public interface IConfigurableHttpClientInitializer
    {
        /// <summary>
        /// 初始化一个创建的HTTP客户端
        /// </summary>
        /// <param name="httpClient"></param>
        void Initialize(ConfigurableHttpClient httpClient);
    }
}
