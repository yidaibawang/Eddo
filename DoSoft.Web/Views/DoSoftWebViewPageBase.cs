using Eddo.Dependency;
using Eddo.Runtime.Session;
using Eddo.Web.Mvc.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoSoft.Web.Views
{
    public abstract class DoSoftWebViewPageBase: DoSoftWebViewPageBase<dynamic>
    {
    }
    public abstract class DoSoftWebViewPageBase<TModel> : EddoWebViewPage<TModel>
    {
        public IEddoSession EddoSession { get; private set; }

        protected DoSoftWebViewPageBase()
        {
            EddoSession = IocManager.Instance.Resolve<IEddoSession>();
       
        }
    }
}