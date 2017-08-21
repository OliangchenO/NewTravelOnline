namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_VisitList
    {
        public int id { get; set; }

        public Guid? orderid { get; set; }

        public int? visitid { get; set; }

        public int? guestid { get; set; }

        public int? rankno { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        public int? Busid { get; set; }

        public int? BusNo { get; set; }
    }
}
