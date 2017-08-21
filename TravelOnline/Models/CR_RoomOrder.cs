namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_RoomOrder
    {
        public int id { get; set; }

        public Guid? OrderId { get; set; }

        public int? lineid { get; set; }

        public int? shipid { get; set; }

        public int? allotid { get; set; }

        public int? roomid { get; set; }

        [StringLength(50)]
        public string roomname { get; set; }

        public int? berth { get; set; }

        public int? price { get; set; }

        public int? thirdprice { get; set; }

        public int? childprice { get; set; }

        public int? rebate { get; set; }

        public int? thirdrebate { get; set; }

        public int? childrebate { get; set; }

        public int? adult { get; set; }

        public int? childs { get; set; }

        public int? peoples { get; set; }

        public int? rooms { get; set; }

        public int? gather { get; set; }

        public int? AllRebate { get; set; }

        public int? mdjs { get; set; }

        public int? thirdmdjs { get; set; }

        public int? childmdjs { get; set; }

        public int? allmdjs { get; set; }
    }
}
