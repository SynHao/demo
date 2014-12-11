using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Logging
{
    /// <summary>
    /// 代表NullLogger它没有做任何记录。
    /// </summary>
    public class NullLogger : ILogger
    {
        #region ILogger成员


        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public ILogger ForType(Type type)
        {
            return new NullLogger();
        }

        public ILogger ForType<T>()
        {
            return new NullLogger();
        }

        public void Info(string message, params object[] formatArgs){ }

        public void Warning(string message, params object[] formatArgs) { }

        public void Debug(string message, params object[] formatArgs) { }

        public void Error(Exception exception, string message, params object[] formatArgs) { }

        public void Error(string message, params object[] formatArgs) { }

        #endregion
    }
}
