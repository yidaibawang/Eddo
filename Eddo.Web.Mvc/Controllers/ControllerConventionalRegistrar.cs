﻿using Castle.MicroKernel.Registration;
using Eddo.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eddo.Web.Mvc.Controllers
{
    public class ControllerConventionalRegistrar: IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<Controller>()
                    .LifestyleTransient()
                );
        }
    }
}
