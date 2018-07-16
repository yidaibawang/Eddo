using Eddo.Permissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions
{
    public interface IEddoModuleMange<TModule, TModuleKey, TModuleInputDto> where TModule : EddoModuleBase<TModuleKey>
        where TModuleKey : struct, IEquatable<TModuleKey>
        where TModuleInputDto : EddoModuleInputDtoBase<TModuleKey>

    {
        IQueryable<TModule> Modules
        {
            get;
        }

    }
}
