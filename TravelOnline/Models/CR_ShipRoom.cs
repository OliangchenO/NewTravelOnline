namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_ShipRoom
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public int? comid { get; set; }

        public int? shipid { get; set; }

        public int? typeid { get; set; }

        [StringLength(50)]
        public string typename { get; set; }

        [StringLength(50)]
        public string roomname { get; set; }

        [StringLength(10)]
        public string roomcode { get; set; }

        [StringLength(50)]
        public string configure { get; set; }

        [StringLength(50)]
        public string deck { get; set; }

        [StringLength(50)]
        public string area { get; set; }

        public int? berth { get; set; }

        [StringLength(500)]
        public string intro { get; set; }

        [StringLength(5)]
        public string rooms { get; set; }
    }
}
