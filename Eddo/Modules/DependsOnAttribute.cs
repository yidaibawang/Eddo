using System;

namespace Eddo.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute:Attribute
    {
        /// <summary>
        /// 类型集合
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

       /// <summary>
       ///传递依赖EddoModule集合
       /// </summary>
       /// <param name="dependedModuleTypes"></param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
