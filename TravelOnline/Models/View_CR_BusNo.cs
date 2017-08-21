namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_CR_BusNo
    {
        public int id { get; set; }

        public int? Lineid { get; set; }

        public int? BusNo { get; set; }

        public int? Berth { get; set; }

        public int? Days { get; set; }

        public int? Visitid { get; set; }

        [StringLength(100)]
        public string vtitle { get; set; }

        [StringLength(100)]
        public string visitname { get; set; }

        public int? nums { get; set; }
    }
}
