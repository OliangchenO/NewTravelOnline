namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Damage
    {
        public int id { get; set; }

        public int? lineid { get; set; }

        public DateTime? begindate { get; set; }

        public DateTime? enddate { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        [StringLength(10)]
        public string rate { get; set; }

        public string infos { get; set; }
    }
}
