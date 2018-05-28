using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.Entities.Auditing
{
      public   interface ICreationAudited: ICreatAudit, ICreatedTime
    {
    }
}
