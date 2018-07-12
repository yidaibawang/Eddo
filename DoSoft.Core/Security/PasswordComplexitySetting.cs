﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoSoft.Core.Security
{
    public class PasswordComplexitySetting
    {
  
  
        public int MinLength { get; set; }

        /// <summary>
        /// Maximum lenght of password
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Force using numbers in password
        /// </summary>
        public bool UseNumbers { get; set; }

        /// <summary>
        /// Force using upper-case letters in password
        /// </summary>
        public bool UseUpperCaseLetters { get; set; }

        /// <summary>
        /// Force using lower case letters in password
        /// </summary>
        public bool UseLowerCaseLetters { get; set; }

        /// <summary>
        /// Force using punctuations in password
        /// </summary>
        public bool UsePunctuations { get; set; }
    }
}
