using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynHaoAPI.SynHao.Apis.Tests
{
    /// <summary>
    /// 标记属性指示方法/类/属性已经取得了较为明显的测试目的。
    /// 标记为内部和薇格特MAKR测试
    /// <code>[assembly: InternalsVisibleTo("Full.Name.Of.Testing.Assembly")]</code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Field)]
    public class VisibleForTestOnly : Attribute
    {
    }
}
