using System.Linq;
using DoSoft.Application.Authorization.Dto;
using Eddo.Applications.Services.Dtos;
using Eddo.Extensions;
using Eddo.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Eddo.AutoMapper;

namespace DoSoft.Application.Authorization
{
    public class UserAppServicecs : DosoftAppServiceBase, IUserAppServicecs
    {
        public UserAppServicecs()
        {

        }
        public async Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input)
        {

            
            var query = UserManager.Users
           .Include(u => u.Roles)
           .WhereIf(input.Role.HasValue, u => u.Roles.Any(r => r.RoleId == input.Role.Value))
           .WhereIf(
               !input.Filter.IsNullOrWhiteSpace(),
               u =>
                   u.Name.Contains(input.Filter) ||
                   u.UserName.Contains(input.Filter) ||
                   u.EmailAddress.Contains(input.Filter)
           );

            if (!input.Permission.IsNullOrWhiteSpace())
            {
                 
            }

            var userCount = await query.CountAsync();
            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var userListDtos = users.MapTo<List<UserListDto>>();
       

            return new PagedResultDto<UserListDto>(
                userCount,
                userListDtos
                );
        }
    }
}
