using SynHaoAPI.SynHao.Apis.Logging;
using SynHaoAPI.SynHao.Apis.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao
{
    /// <summary>
    /// 定义该库在其中运行的环境，它允许设置自定义的记录器
    /// </summary>
    public class ApplicationContext
    {
        [VisibleForTestOnly]
        internal static ILogger logger;

        /// <summary>
        /// 返回在这个应用程序中使用的记录
        /// </summary>
        /// <remarks>如果没有其他设置的话，他将创建一个<see cref="NullLogger"/></remarks>
        public static ILogger Logger
        {
            get
            {
                // 如果没有其他的设置的话，注册一个空记录
                return logger ?? (logger=new NullLogger());
            }
        }

        /// <summary>
        /// 注册与此应用程序上下文相同的几率
        /// </summary>
        /// <exception cref="InvalidOperationException">抛出一个异常当记录器已经存在的话</exception>
        public static void RegisterLogger(ILogger loggerToRegister)
        {
            // TODO(peleyal): 重新考虑库中为什么应该只包含一个记录。也可以考虑使用跟踪
            if (logger!=null&&!(logger is NullLogger))
            {
                throw new InvalidOperationException("记录器已经记录这个内容");
            }
            logger = loggerToRegister;
        }
    }
}
