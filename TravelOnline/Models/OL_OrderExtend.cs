using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.Models
{
    public partial class OL_OrderExtend
    {
        public Guid OrderId { get; set; }
        public string ExtType { get; set; }
        public byte? ExtId { get; set; }
        public string ExtContent { get; set; }
        public DateTime? InputTime { get; set; }
    }
}