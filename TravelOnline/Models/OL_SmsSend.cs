namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_SmsSend
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public string mobile { get; set; }

        public string smscontent { get; set; }

        public DateTime? sendtime { get; set; }

        public int? sendid { get; set; }

        [StringLength(20)]
        public string flag { get; set; }

        [StringLength(50)]
        public string extinfo { get; set; }
    }
}
