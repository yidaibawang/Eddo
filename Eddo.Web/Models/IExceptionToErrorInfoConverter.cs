﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Models
{
    public interface IExceptionToErrorInfoConverter
    {
        IExceptionToErrorInfoConverter Next { set; }

        ErrorInfo Convert(Exception exception);
    }
}
