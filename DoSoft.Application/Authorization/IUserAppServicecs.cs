using DoSoft.Application.Authorization.Dto;
using Eddo.Applications.Services;
using Eddo.Applications.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoSoft.Application.Authorization
{
    public  interface IUserAppServicecs: IApplicationService
    {
        Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);
        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);
    }
}
