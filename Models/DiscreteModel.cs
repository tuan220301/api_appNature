using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_appNature.Models
{
    public class DiscreteModel
    {
        public int DiscreteID { get; set; }
        public string DiscreteNbr { get; set; } = string.Empty;
        public int InventoryID { get; set; }
        public string InventoryCD { get; set; } = string.Empty;
        public string InventoryDesc { get; set; } = string.Empty;
        public decimal DiscreteQty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<RoutingDiscreteModel> Routings { get; set; }

        public string DepartmentID { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
    }
}