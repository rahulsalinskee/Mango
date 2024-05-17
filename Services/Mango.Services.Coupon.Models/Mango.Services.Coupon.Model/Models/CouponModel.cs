using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.Coupon.Model.Models
{
    public class CouponModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinimumAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCouponActive { get; set; } = false;
        public DateTime ExpiryDate { get; set; }
    }
}
