using Eddo.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.AutoMapper
{
     public static  class EddoAutoMapperConfigurationExtensions
    {
        public static IEddoAutoMapperConfiguration EddoAutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.EddoConfiguration.Get<IEddoAutoMapperConfiguration>();
        }
    }
}
