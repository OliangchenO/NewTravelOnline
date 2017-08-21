namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_RoomOrderList
    {
        public int id { get; set; }

        public Guid? OrderId { get; set; }

        public int? lineid { get; set; }

        [StringLength(50)]
        public string roomname { get; set; }

        public int? peoples { get; set; }

        public int? rooms { get; set; }

        public byte? OrderFlag { get; set; }

        public int? gather { get; set; }

        public int? AllRebate { get; set; }

        public int? roomid { get; set; }
    }
}
