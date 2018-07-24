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
using DoSoft.Core.UserManagerment;
using Microsoft.AspNet.Identity;

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
        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        {
            var output = new GetUserForEditOutput();
            if (!input.Id.HasValue)
            {
                //Creating a new user
                output = new GetUserForEditOutput
                {
                    IsActive = true,
                    ShouldChangePasswordOnNextLogin = false
            
                };

            }
            else
            {
                //Editing an existing user
                var user = await UserManager.GetUserByIdAsync(input.Id.Value);

                output= user.MapTo<GetUserForEditOutput>();
          
            }
            return output;

        }
        public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            if (!input.User.Id.HasValue)
            {
               await  CreateUserAsync(input);
            }
        }
        protected virtual async Task CreateUserAsync(CreateOrUpdateUserInput input)
        {
         
            var user = input.User.MapTo<User>(); //Passwords is not mapped (see mapping configuration)

            //Set password
            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.User.Password));
            }
            else
            {
                input.User.Password = User.DefaultPassword;
            }

            user.Password = new PasswordHasher().HashPassword(input.User.Password);
            //Assign roles
        
            CheckErrors(await UserManager.CreateAsync(user));        
        }

    }
}
