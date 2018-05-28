using Eddo.AutoMapper;
using Eddo.Domain.Entities;

namespace Web.Core.Entity
{
    [AutoMap(typeof(User))]
    public  class UserCacheItem
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
    }
}
