namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_RoomList
    {
        public int id { get; set; }

        public Guid? OrderId { get; set; }

        public int? lineid { get; set; }

        public int? planid { get; set; }

        public int? allotid { get; set; }

        public int? typeid { get; set; }

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

        public int? peoples { get; set; }

        [StringLength(1)]
        public string orderflag { get; set; }

        [StringLength(50)]
        public string guestids { get; set; }

        public int? adults { get; set; }

        public int? childs { get; set; }

        [StringLength(10)]
        public string roomcode { get; set; }

        [StringLength(10)]
        public string roomno { get; set; }

        public int? roomnoid { get; set; }

        [StringLength(1)]
        public string MergeFlag { get; set; }

        [StringLength(1)]
        public string BedType { get; set; }

        public int? mdjs { get; set; }

        public int? thirdmdjs { get; set; }

        public int? childmdjs { get; set; }

        public int? fourprice { get; set; }

        public int? fourchild { get; set; }

        public int? fourrebate { get; set; }

        public int? fourchildrebate { get; set; }
    }
}
