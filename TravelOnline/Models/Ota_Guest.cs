namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ota_Guest
    {
        public int id { get; set; }

        public int? orderid { get; set; }

        [StringLength(50)]
        public string guestname { get; set; }

        [StringLength(1)]
        public string sex { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string mobile { get; set; }

        [StringLength(50)]
        public string tel { get; set; }

        [StringLength(500)]
        public string memo { get; set; }

        [StringLength(50)]
        public string idcard { get; set; }

        [StringLength(50)]
        public string cardtype { get; set; }
    }
}
