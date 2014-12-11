using SynHaoAPI.SynHao.Apis.Logging;
using SynHaoAPI.SynHao.Apis.Tests;
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
    /// 他包含了我们HTTP请求的主要逻辑消息处理程序。
    /// 他包含一系列<see cref="IHttpUnsuccessfulResponseHandler"/>处理异常反应
    /// <see cref="IHttpExceptionHandler"/>处理异常请求
    /// <see cref="IHttpExecuteInterceptor"/>表示拦截请求之前，他已经被发送的服务器
    /// 同时也包含尝试次数，重定向等重要的功能
    /// </summary>
    public class ConfigurableMessageHandler:DelegatingHandler
    {
        /// <summary>
        /// 记录器类
        /// </summary>
        private static readonly ILogger Logger = ApplicationContext.Logger.ForType<ConfigurableMessageHandler>();

        /// <summary>
        /// 允许的最大次数
        /// </summary>
        [VisibleForTestOnly]
        internal const int MaxAllowedNumTries = 20;

        /// <summary>
        /// 当前的库版本
        /// </summary>
        private static readonly string ApiVersion = SynHao.Apis.Util.Utilities.GetLibraryVersion();

        /// <summary>
        /// 设置程序代理
        /// </summary>
        private static readonly string UserAgentSuffix = "google-api-dotnet-client/" + ApiVersion + "(gzip)";

        #region IHttpUnsuccessfulResponseHandler, IHttpExceptionHandler 和 IHttpExecuteInterceptor列表

        /// <summary>
        /// <see cref="IHttpUnsuccessfulResponseHandler"/>列表
        /// </summary>
        private readonly IList<IHttpUnSuccessfulResponseHandler> unsuccessfulResponseHandlers =
            new List<IHttpUnSuccessfulResponseHandler>();

        /// <summary>
        /// <see cref="IHttpExceptionHandler"/>列表
        /// </summary>
        private readonly IList<IHttpExceptionHandler> exceptionHandlers =
            new List<IHttpExceptionHandler>();

        /// <summary>
        /// <see cref="IHttpExecuteInterceptor"/>列表
        /// </summary>
        private readonly IList<IHttpExecuteInterceptor> executeInterceptors =
            new List<IHttpExecuteInterceptor>();

        /// <summary>
        /// 获取列表<see cref="IHttpUnSuccessfulResponseHandler"/>
        /// </summary>
        public IList<IHttpUnSuccessfulResponseHandler> UnsuccessfulResponseHandlers
        {
            get { return unsuccessfulResponseHandlers; }
        }

        /// <summary>
        /// 获取列表<see cref="IHttpExceptionHandler"/>
        /// </summary>
        public IList<IHttpExceptionHandler> ExceptionHandlers
        {
            get { return exceptionHandlers; }
        }

        /// <summary>
        /// 获取列表<see cref="IHttpExecuteInterceptor"/>
        /// </summary>
        public IList<IHttpExecuteInterceptor> ExecuteInterceptors
        {
            get { return executeInterceptors; }
        }

        #endregion

        /// <summary>
        /// 尝试的数量，默认是<c>3</c>
        /// </summary>
        private int numTries = 3;

        /// <summary>
        /// 获取或设置被允许尝试的次数。
        /// 处理被终止前的HTTP的响应或异常，将作为
        /// <see cref="IHttpUnSuccessfulResponseHandler"/>
        /// <see cref="IHttpExceptionHandler"/>
        /// 设置为<c>1</c>时不尝试。
        /// 默认值为<c>3</c>
        /// </summary>
        /// <remarks>
        /// 有<see cref="NumRedirects"/>定义重定向（3xx）的次数。此属性只定义>=400的响应。
        /// 如：当你设置<see cref="NumTries"/>为1，且<see cref="NumRedirects"/>为5时，
        /// 库可以发送5次重定向，但不会发送任何因为请求错误产生HTTP错误请求代码的重试请求
        /// </remarks>
        public int NumTries
        {
            get { return numTries; }

            set
            {
                if (value > MaxAllowedNumTries || value < 1)
                {
                    throw new ArgumentOutOfRangeException("NumTries");
                }
                numTries = value;
            }
        }

        /// <summary>
        /// 重定向被允许的次数。默认是<c>10</c>
        /// </summary>
        private int numRedirects = 10;


        /// <summary>
        /// 获取或设置被允许重定向的次数。默认值是<c>10</c>
        /// <see cref="NumTries"/>可以看到更多的信息
        /// </summary>
        public int NumRedirects
        {
            get { return numRedirects; }
            set
            {
                if (value > MaxAllowedNumTries || value < 1)
                {
                    throw new ArgumentOutOfRangeException("NumRedirects");
                }
                numRedirects = value;
            }
        }

        /// <summary>
        /// 获取和设置处理程序师傅应该遵循重定向。默认值为<c>true</c>
        /// </summary>
        public bool FollowRedirect { get; set; }

        /// <summary>
        /// 获取和设置记录器是否启用。默认<c>true</c>
        /// </summary>
        public bool IsLoggingEnabled { get; set; }

        /// <summary>
        /// 获取和设置应用被应用于应用用户代理的用户名称
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// 构造一个新的消息处理程序
        /// </summary>
        /// <param name="httpMessageHandler"></param>
        public ConfigurableMessageHandler(HttpMessageHandler httpMessageHandler)
            : base(httpMessageHandler)
        {
            // 设置默认值
            FollowRedirect = true;
            IsLoggingEnabled = true;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var loggable = IsLoggingEnabled && Logger.IsDebugEnabled;

            int triesRemaining = NumTries;
            int redirectRemaining = NumRedirects;

            Exception lastException = null;

            // 设置代理头部
            var userAgent = (ApplicationName == null ? "" : ApplicationName + " ") + UserAgentSuffix;

            request.Headers.Add("User-Agent", userAgent);

            HttpResponseMessage response = null;
            do // 当 triesRemaining > 0
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (response != null)
                {
                    response.Dispose();
                    response = null;
                }
                lastException = null;

                // 拦截的请求
                foreach (var interceptor in executeInterceptors)
                {
                    await interceptor.InterceptAsync(request, cancellationToken).ConfigureAwait(false);
                }

                try
                {
                    // 发送请求
                    response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }

                // 减少重试的次数
                if (response == null || ((int)response.StatusCode >= 400 || (int)response.StatusCode <= 200))
                {
                    triesRemaining--;
                }

                // 抛出异常，并试图重试
                if (response == null)
                {
                    var exceptionHandled = false;

                    // 尝试处理每个处理器的异常
                    foreach (var handler in exceptionHandlers)
                    {
                        exceptionHandled |= await handler.HandleExceptionAsync(new HandleExceptionArgs
                        {
                            Request = request,
                            Exception = lastException,
                            TotalTries = NumTries,
                            CurrentFailedTry = NumTries - triesRemaining,
                            CancellationToken = cancellationToken
                        }).ConfigureAwait(false);
                    }
                    if (!exceptionHandled)
                    {
                        Logger.Error(lastException,
                            "他不处理在执行HTTP请求是抛出的异常");
                        throw lastException;
                    }
                    else if (loggable)
                    {
                        Logger.Debug("异常{0}被抛出，但他是由一个异常处理程序进行处理", lastException.Message);
                    }
                }
                else
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // 请求成功，不需要重试
                        triesRemaining = 0;
                    }
                    else
                    {
                        bool errorHandled = false;

                        // 处理每个异常的HTTP响应
                        foreach (var handler in unsuccessfulResponseHandlers)
                        {
                            errorHandled |= await handler.HandleResponseAsync(new HandleUnsuccessfulResponseArgs
                            {
                                Request = request,
                                Response = response,
                                TotalTries = NumTries,
                                CurrentFailedTry = NumTries - triesRemaining,
                                CancellationToken = cancellationToken
                            }).ConfigureAwait(false);
                        }

                        if (!errorHandled)
                        {
                            if (FollowRedirect && HandleRedirect(response))
                            {
                                if (redirectRemaining-- == 0)
                                {
                                    triesRemaining = 0;
                                }

                                errorHandled = true;
                                if (loggable)
                                {
                                    Logger.Debug("重定向成功，重定向到{0}", response.Headers.Location);
                                }
                            }
                            else
                            {
                                if (loggable)
                                {
                                    Logger.Debug("没有处理的异常反应。返回值是{0}", response.StatusCode);
                                }
                                triesRemaining = 0;
                            }
                        }
                        else if (loggable)
                        {
                            Logger.Debug("异常反应是不成功响应处理程序处理，处理代码是{0}", response.StatusCode);
                        }
                    }
                }
            } while (triesRemaining > 0);

            if (response == null)
            {
                Logger.Error(lastException, "抛出一个HTTP请求的异常");
                throw lastException;
            }
            else if (!response.IsSuccessStatusCode)
            {
                Logger.Debug("不处理的异常被返回，返回值{0}", response.StatusCode);
            }

            return response;
        }

        private bool HandlerRedirect(HttpResponseMessage message)
        {
            var uri = message.Headers.Location;
            if(!message.IsRedirectStatusCode)
        }
    }
}
