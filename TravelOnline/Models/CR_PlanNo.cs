namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_PlanNo
    {
        public int id { get; set; }

        public int? Lineid { get; set; }

        public int? PlanNo { get; set; }

        public int? Berth { get; set; }

        [StringLength(50)]
        public string PlanCode { get; set; }

        [StringLength(100)]
        public string memo { get; set; }
    }
}
