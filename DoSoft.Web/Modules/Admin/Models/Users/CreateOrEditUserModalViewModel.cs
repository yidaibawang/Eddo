using DoSoft.Application.Authorization.Dto;
using DoSoft.Core.UserManagerment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eddo.AutoMapper;
namespace DoSoft.Admin.Models.Users
{
    [AutoMapFrom(typeof(GetUserForEditOutput))]
    public class CreateOrEditUserModalViewModel: UserEditDto
    {
        public bool IsEditMode
        {
            get { return Id.HasValue; }
        }
        public CreateOrEditUserModalViewModel(GetUserForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}