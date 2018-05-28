using Eddo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Core.Order
{    
    [Table("OrderDetail")]
    public class OrderDetail:Entity<int>
    {    
        [Required]
        public virtual int? OrderInfoId { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        public int ProductId { get; set; }
        public int ProductCount { get; set; }

        public string Remark { get; set; }
    }
}
