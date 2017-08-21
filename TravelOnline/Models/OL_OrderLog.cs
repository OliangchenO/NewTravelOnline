using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace TravelOnline.Models
{
    public partial class OL_OrderLog
    {
        public Guid OrderId { get; set; }
        public DateTime? LogTime { get; set; }
        public string LogContent { get; set; }
    }
}