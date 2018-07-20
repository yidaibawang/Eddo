using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoSoft.Admin.Models.Users
{
    public class UserListModel
    {
        public UserListModel()
        {

        }
        [Display(Name ="用户名")]
        public string UserName { get; set; }
    }
}