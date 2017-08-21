namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_MisDoError
    {
        public int id { get; set; }

        public Guid? orderid { get; set; }

        [StringLength(50)]
        public string adjustflag { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        public DateTime? inputtime { get; set; }

        public DateTime? dotime { get; set; }

        public string infos { get; set; }

        [StringLength(50)]
        public string douser { get; set; }
    }
}
