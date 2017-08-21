namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Rebate
    {
        public int id { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        public int? lineid { get; set; }

        public int? roomid { get; set; }

        public int? allotid { get; set; }

        public DateTime? begindate { get; set; }

        public DateTime? enddate { get; set; }

        [StringLength(100)]
        public string infos { get; set; }

        public int? price { get; set; }
    }
}
