namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pre_Ticket
    {
        public int id { get; set; }

        public int? pid { get; set; }

        public Guid? uid { get; set; }

        [StringLength(50)]
        public string uno { get; set; }

        public int? par { get; set; }

        public int? amount { get; set; }

        public Guid? userid { get; set; }

        public DateTime? begindate { get; set; }

        public DateTime? enddate { get; set; }

        public DateTime? inputdate { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        public DateTime? usedate { get; set; }

        [StringLength(1)]
        public string deduction { get; set; }

        [StringLength(1)]
        public string range { get; set; }

        [StringLength(500)]
        public string product { get; set; }

        public int? AutoId { get; set; }

        public Guid? OrderId { get; set; }

        [StringLength(50)]
        public string UserEmail { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        public DateTime? pbdate { get; set; }

        public DateTime? pedate { get; set; }

        [StringLength(1)]
        public string sellflag { get; set; }

        [StringLength(50)]
        public string pre_no { get; set; }
    }
}
