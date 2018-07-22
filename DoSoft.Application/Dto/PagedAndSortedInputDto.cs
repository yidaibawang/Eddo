using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Applications.Services.Dtos;
namespace DoSoft.Application.Dto
{
    [Serializable]
    public class PagedAndSortedInputDto: PagedInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }

        public PagedAndSortedInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }
    }
}
