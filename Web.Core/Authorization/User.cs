using Eddo.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Eddo.Permissions.Authorization.Users;
using System;
using Eddo.Extensions;
using Microsoft.AspNet.Identity;

namespace Web.Core
{

    public class User : EddoUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }
        public User()
        {

        }
        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };

            return user;
        }
    }
}
