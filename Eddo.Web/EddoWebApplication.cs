using Eddo.Dependency;
using Eddo.Reflection;
using System;
using System.Web;
namespace Eddo.Web
{     
    public class EddoWebApplication:HttpApplication
    {
        protected EddoBootstrapper EddoBootstrapper { get; private set; }

        protected EddoWebApplication()
        {
            EddoBootstrapper = new EddoBootstrapper();
        }
        //程序开始
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            EddoBootstrapper.IocManager.RegisterIfNot<IAssemblyFinder, WebAssemblyFinder>();
            EddoBootstrapper.Initialize();
        }

        public void Application_Start(object sender, object e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 程序结束
        /// </summary>
        protected virtual void Application_End(object sender, EventArgs e)
        {
            EddoBootstrapper.Dispose();
        }

        /// <summary>
        /// 会话开始
        /// </summary>
        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 会话结束
        /// </summary>
        protected virtual void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 开始接受参数
        /// </summary>
        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected virtual void SetCurrentCulture()
        {

        }

        /// <summary>
        /// 结束参数
        /// </summary>
        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 在安全模块建立起当前用户的有效的身份时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 程序错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_Error(object sender, EventArgs e)
        {

        }
    }
}
