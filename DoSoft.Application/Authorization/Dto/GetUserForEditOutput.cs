using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoSoft.Core.UserManagerment;
using Eddo.AutoMapper;
namespace DoSoft.Application.Authorization.Dto
{   
    [AutoMapFrom(typeof(User))]
    public class GetUserForEditOutput: UserEditDto
    {

    }
}
