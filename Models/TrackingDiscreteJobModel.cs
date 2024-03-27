using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_appNature.Models
{
    public class TrackingDiscreteJobModel
    {
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public string DepartmentID { get; set; } = string.Empty;
        [Required]
        public int DiscreteID { get; set; }
        [Required]
        public int RoutingNO { get; set; }
        [Required]
        public decimal PrevQty { get; set; }
        [Required]
        public decimal Qty { get; set; }
        [Required]
        public DateTime TrackingDate { get; set; }
    }
}