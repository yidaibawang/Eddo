﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoSoft.Admin.Models.Common
{
    public class ModalHeaderViewModel
    {
        public string Title { get; set; }

        public ModalHeaderViewModel(string title)
        {
            Title = title;
        }
    }
}