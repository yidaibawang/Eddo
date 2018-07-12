using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoSoft.Web.Models.Account
{
    public class LoginFormViewModel
    {
        public string SuccessMessage { get; set; }

        public string UserNameOrEmailAddress { get; set; }

        public bool IsSelfRegistrationEnabled { get; set; }
    }
}