namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Appraise
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public Guid? orderid { get; set; }

        [StringLength(10)]
        public string types { get; set; }

        public DateTime? inputtime { get; set; }

        public string contents { get; set; }

        public int? score { get; set; }

        [StringLength(1)]
        public string flag { get; set; }
    }
}
