using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_appNature.Models
{
    public class RoutingDiscreteModel
    {
        public int RoutingID { get; set; }
        public int RoutingNo { get; set; }
        public string RoutingCode { get; set; } = string.Empty;
        public string RoutingName { get; set; } = string.Empty;
    }
}