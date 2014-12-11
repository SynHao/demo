using NUnit.Framework;
using SynHaoAPI.SynHao.Apis.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Tests
{
    /// <summary>
    /// 测试<see cref="Google.ApplicationContent"/>
    /// </summary>
    [TestFixture]
    public class ApplicationContextTests
    {
        private class MockLogger : ILogger
        {
            public bool IsDebugEnabled
            {
                get { throw new NotImplementedException(); }
            }

            bool ILogger.IsDebugEnabled
            {
                get { throw new NotImplementedException(); }
            }

            public ILogger ForType(Type type)
            {
                throw new NotImplementedException();
            }

            public ILogger ForType<T>()
            {
                throw new NotImplementedException();
            }

            public void Info(string message, params object[] formatArgs)
            {
                throw new NotImplementedException();
            }

            public void Warning(string message, params object[] formatArgs)
            {
                throw new NotImplementedException();
            }

            public void Debug(string message, params object[] formatArgs)
            {
                throw new NotImplementedException();
            }

            public void Error(Exception exception, string message, params object[] formatArgs)
            {
                throw new NotImplementedException();
            }

            public void Error(string message, params object[] formatArgs)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 检查注册的默认记录器
        /// </summary>
        [Test]
        public void GetLoggerDefaultTest()
        {
            Assert.IsInstanceOf<NullLogger>(ApplicationContext.Logger);
        }

        /// <summary>
        /// 确认以前没有登记过RegisterLogger方法不会失败
        /// </summary>
        [Test]
        public void RegisterLoggerTest()
        {
            // 初始化一个NullLogger:
            Assert.IsInstanceOf<NullLogger>(ApplicationContext.Logger);

            // 注册一个新的记录器：
            Assert.DoesNotThrow(() => ApplicationContext.RegisterLogger(new MockLogger()));

            // 测试失败：
            Assert.Throws<InvalidOperationException>(() => ApplicationContext.RegisterLogger(new MockLogger()));

            // 改回默认的记录器
            ApplicationContext.logger = null;
        }
    }
}
