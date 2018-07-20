using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class RegisterResultViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }
    }
}