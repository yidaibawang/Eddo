using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Applications.Services.Dtos
{   
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        /// <summary>
        /// 列表
        /// </summary>
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        /// 初始化 <see cref="ListResultDto{T}"/> object.
        /// </summary>
        public ListResultDto()
        {

        }

        /// <summary>
        ///初始化
        /// </summary>
        /// <param name="items">List of items</param>
        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
