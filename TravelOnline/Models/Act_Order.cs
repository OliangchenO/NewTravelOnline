namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Act_Order
    {
        [Key]
        public int ActOrderID { get; set; }

        public int? GuestID { get; set; }

        [StringLength(20)]
        public string GuestName { get; set; }

        public int? ActInfoMain_ID { get; set; }

        [StringLength(50)]
        public string ActName { get; set; }

        public DateTime? JoinTime { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public int? OL_OrderID { get; set; }

        [StringLength(30)]
        public string OrderMobile { get; set; }
    }
}
