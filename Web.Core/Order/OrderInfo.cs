using System;
using Eddo.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Web.Core.Order
{
    [Table("OrderInfo")]
    public class OrderInfo:Entity<int>
    {   
        [Required]
        public string No { get; set; }
        public string Status { get; set; }
        public int ProductCount { get; set; }
        public string Remark { get; set; }

        [ForeignKey("OrderInfoId")]
        public virtual ICollection<OrderDetail> Details { get; set; }
        public DateTime? OrderDate { get; set; }

        public OrderInfo()
        {
            Details = new Collection<OrderDetail>();
        }
    }
}
