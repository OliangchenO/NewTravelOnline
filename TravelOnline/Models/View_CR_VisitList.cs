namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_CR_VisitList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public Guid? orderid { get; set; }

        public int? visitid { get; set; }

        public int? guestid { get; set; }

        public int? rankno { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        public int? price { get; set; }

        public int? Busid { get; set; }

        public int? BusNo { get; set; }

        public int? LineID { get; set; }

        [StringLength(30)]
        public string OrderName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AutoId { get; set; }

        [StringLength(100)]
        public string visitname { get; set; }

        [StringLength(20)]
        public string guestname { get; set; }
    }
}
