namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_VisitReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public Guid? orderid { get; set; }

        public int? visitid { get; set; }

        public int? guestid { get; set; }

        public int? rankno { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        public int? Busid { get; set; }

        public int? BusNo { get; set; }

        public int? lineid { get; set; }

        public int? days { get; set; }

        [StringLength(100)]
        public string vtitle { get; set; }

        [StringLength(100)]
        public string visitname { get; set; }

        [StringLength(50)]
        public string stay { get; set; }

        [StringLength(50)]
        public string sight { get; set; }

        [StringLength(50)]
        public string dinner { get; set; }

        [Column(TypeName = "ntext")]
        public string intro { get; set; }

        public int? price { get; set; }

        public int? nums { get; set; }

        [StringLength(50)]
        public string vdate { get; set; }

        [StringLength(100)]
        public string vmemo { get; set; }
    }
}
