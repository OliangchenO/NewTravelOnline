namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_Act_GuestInfo_a
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AutoId { get; set; }

        public byte? PayFlag { get; set; }

        [StringLength(11)]
        public string OrderMobile { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GuestID { get; set; }

        [StringLength(20)]
        public string GuestName { get; set; }

        public DateTime? BirthDay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Age { get; set; }

        [StringLength(10)]
        public string roomno { get; set; }

        public int? PlanNo { get; set; }
    }
}
