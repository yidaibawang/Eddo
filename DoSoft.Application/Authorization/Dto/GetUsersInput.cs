using DoSoft.Application.Dto;
using Eddo.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoSoft.Application.Authorization.Dto
{
    [Serializable]
    public class GetUsersInput: PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public string Permission { get; set; }

        public int? Role { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name ";
            }
        }
    }
}
