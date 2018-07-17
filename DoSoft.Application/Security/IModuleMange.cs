using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Permissions;
using DoSoft.Core.Security.Entities;
using DoSoft.Application.Security.Dtos;

namespace DoSoft.Application.Security
{
    public interface IModuleMange:IEddoModuleMange<Module,int, ModuleInputDto>
    {
    }
}
