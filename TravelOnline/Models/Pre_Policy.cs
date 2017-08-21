namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pre_Policy
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        [StringLength(1)]
        public string sellflag { get; set; }

        [StringLength(1)]
        public string deduction { get; set; }

        [StringLength(1)]
        public string range { get; set; }

        [StringLength(500)]
        public string product { get; set; }

        public DateTime? begindate { get; set; }

        public DateTime? enddate { get; set; }

        public int? sellprice { get; set; }

        public int? par { get; set; }

        public int? amount { get; set; }

        [StringLength(100)]
        public string memo { get; set; }

        public Guid? userid { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        public DateTime? inputdate { get; set; }

        [StringLength(50)]
        public string picurl { get; set; }

        public DateTime? pbdate { get; set; }

        public DateTime? pedate { get; set; }

        public int? sellnums { get; set; }

        [StringLength(50)]
        public string pre_no { get; set; }
    }
}
