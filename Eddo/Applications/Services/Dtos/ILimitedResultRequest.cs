﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Applications.Services.Dtos
{
   public  interface ILimitedResultRequest
    {
        int MaxResultCount { get; set; }
    }
}
