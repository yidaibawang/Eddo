using Eddo.Dependency;
using Eddo.Runtime.Session;
using Eddo.Web.Mvc.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoSoft.Admin.Views
{

    public abstract class DoSoftWebAdminViewPageBase : DoSoftWebAdminViewPageBase<dynamic>
    {
    }
    public abstract class DoSoftWebAdminViewPageBase<TModel> : EddoWebViewPage<TModel>
    {
        public IEddoSession EddoSession { get; private set; }

        protected DoSoftWebAdminViewPageBase()
        {
            EddoSession = IocManager.Instance.Resolve<IEddoSession>();

        }
    }
}