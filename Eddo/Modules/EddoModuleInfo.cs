using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Modules
{
    public class EddoModuleInfo
    {   
        /// <summary>
        /// 空间名
        /// </summary>
        public Assembly Assembly { get; private set; }
        /// <summary>
        /// 类型名
        /// </summary>
        public Type Type { get; private set; }
        public EddoModule Instance { get; private set; }

        /// <summary>
        /// 获取所有依赖的模块
        /// </summary>
        public List<EddoModuleInfo> Dependencies { get; private set; }

        /// <summary>
        /// 初始化模块信息
        /// </summary>
        /// <param name="instance"></param>
        public EddoModuleInfo(EddoModule instance)
        {
            Dependencies = new List<EddoModuleInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        public override string ToString()
        {
            return string.Format("{0}", Type.AssemblyQualifiedName);
        }
    }
}
