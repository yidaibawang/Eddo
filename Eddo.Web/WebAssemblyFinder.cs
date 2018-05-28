using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eddo.Reflection;
using System.Web;
using System.Web.Compilation;
using System.IO;
namespace Eddo.Web
{
    public class WebAssemblyFinder : IAssemblyFinder
    {

        public static WebAssemblyFinder Instance { get { return SingletonInstance; } }
        private static readonly WebAssemblyFinder SingletonInstance = new WebAssemblyFinder();
        //查找当前目录
        SearchOption FindAssembliesSearchOption = SearchOption.TopDirectoryOnly;
        /// <summary>
        /// 获取web下的空间文件
        /// </summary>
        /// <returns></returns>
        public List<Assembly> GetAllAssemblies()
        {
            var assembliesInBinFolder = new List<Assembly>();
            //获取所有引用空间
            var allReferencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            //获取运行目录下所有dll文件
            var dllFiles = Directory.GetFiles(HttpRuntime.AppDomainAppPath+"bin\\","*.dll", FindAssembliesSearchOption).ToList();
            foreach (string dllfile in dllFiles)
            {    
                var localAssemblie = allReferencedAssemblies.FirstOrDefault(asm => AssemblyName.ReferenceMatchesDefinition(asm.GetName(), AssemblyName.GetAssemblyName(dllfile)));
                if (localAssemblie != null)
                {
                    assembliesInBinFolder.Add(localAssemblie);
                }
            }
            return assembliesInBinFolder;
        }
    }
}
