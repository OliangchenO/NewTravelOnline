namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_RoomAllot
    {
        public int id { get; set; }

        [StringLength(1)]
        public string allotflag { get; set; }

        public int? lineid { get; set; }

        public int? shipid { get; set; }

        public int? roomid { get; set; }

        public int? companyid { get; set; }

        [StringLength(100)]
        public string company { get; set; }

        public int? typeid { get; set; }

        [StringLength(50)]
        public string typename { get; set; }

        [StringLength(50)]
        public string roomname { get; set; }

        [StringLength(10)]
        public string roomcode { get; set; }

        public int? berth { get; set; }

        public int? nums { get; set; }

        public int? price { get; set; }

        public int? thirdprice { get; set; }

        public int? childprice { get; set; }

        public int? rebate { get; set; }

        public int? thirdrebate { get; set; }

        public int? childrebate { get; set; }

        [StringLength(50)]
        public string area { get; set; }

        [StringLength(50)]
        public string deck { get; set; }

        [StringLength(1)]
        public string sellflag { get; set; }

        [StringLength(1)]
        public string recommend { get; set; }

        public int? mdjs { get; set; }

        public int? thirdmdjs { get; set; }

        public int? childmdjs { get; set; }

        public int? fourprice { get; set; }

        public int? fourchild { get; set; }

        public int? fourrebate { get; set; }

        public int? fourchildrebate { get; set; }

        public int? parentid { get; set; }

        [StringLength(50)]
        public string pricename { get; set; }
    }
}
