using DoSoft.Core.UserManagerment;
using Eddo.Applications.Services.Dtos;
using Eddo.AutoMapper;
using Eddo.Permissions.Authorization.Users;
using System;
using System.Collections.Generic;

namespace DoSoft.Application.Authorization.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserListDto: EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public List<UserListRoleDto> Roles { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }
        [AutoMapFrom(typeof(UserRole))]
        public class UserListRoleDto
        {
            public int RoleId { get; set; }

            public string RoleName { get; set; }
        }
    }
}
