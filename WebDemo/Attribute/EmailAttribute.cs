using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDemo.Attribute
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base("@验证表达式")
        {
        }
    }
}