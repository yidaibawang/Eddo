using Eddo.Dependency;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eddo.Web.Mvc.Controllers
{
    public class WindsorControllerFactory: DefaultControllerFactory
    {
        /// <summary>
        /// DI注入器
        /// </summary>
        private readonly IIocResolve _iocManager;

        /// <summary>
        /// 创建实例化WindsorControllerFactory
        /// </summary>
        /// <param name="iocManager">Reference to DI kernel</param>
        public WindsorControllerFactory(IIocResolve iocManager)
        {
            _iocManager = iocManager;
        }

        /// <summary>
        ///  重写ioc注册
        /// </summary>
        /// <param name="controller">Controller instance</param>
        public override void ReleaseController(IController controller)
        {
            _iocManager.Release(controller);
        }

        /// <summary>
        /// 查询IController依赖对象
        /// </summary>
        /// <param name="requestContext">Request context</param>
        /// <param name="controllerType">Controller type</param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            return _iocManager.Resolve<IController>(controllerType);
        }
    }
}
