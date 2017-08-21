namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_RoomNo
    {
        public int id { get; set; }

        public int? Lineid { get; set; }

        public int? RoomId { get; set; }

        [StringLength(10)]
        public string RoomNo { get; set; }

        [StringLength(10)]
        public string roomcode { get; set; }

        [StringLength(50)]
        public string RoomName { get; set; }

        public int? berth { get; set; }

        public int? Nums { get; set; }

        public int? Listid { get; set; }

        public int? Mergeid { get; set; }

        [StringLength(1)]
        public string Flag { get; set; }

        public DateTime? InputDate { get; set; }

        public DateTime? DoDate { get; set; }

        [StringLength(20)]
        public string BookingNo { get; set; }
    }
}
