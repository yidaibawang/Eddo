namespace Eddo.Web.Api.Controllers.Dynamic
{
    /// <summary>
    /// 此类用作所有动态创建的ApiControllers的基类。
    /// </summary>
    /// <typeparam name="T">代理对象的类型</typeparam>
    /// <remarks>
    /// 动态ApiController 将透明地公开对象（通常是Application Service类）
    /// 公开给远程客户端。
    /// </remarks>
    public class DynamicApiController<T>:EddoApiController,IDynamicApiController
    {
    }
}
