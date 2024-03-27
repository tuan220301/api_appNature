using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_appNature.Models
{
    public class AddDiscreteModel
    {
        public int CompanyID { get; set; }
        public string DepartmentID { get; set; } = string.Empty;
        public int DiscreteID { get; set; }
        public int RoutingNO { get; set; }
        public decimal PrevQty { get; set; }
        public decimal Qty { get; set; }
        public DateTime TrackingDate { get; set; }
    }
}