namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Pic
    {
        public int id { get; set; }

        public int? shipid { get; set; }

        public int? roomid { get; set; }

        public int? deck { get; set; }

        public int? days { get; set; }

        [StringLength(10)]
        public string pictype { get; set; }

        [StringLength(50)]
        public string roomtype { get; set; }

        [StringLength(50)]
        public string cname { get; set; }

        [StringLength(100)]
        public string picurl { get; set; }
    }
}
