namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_CR_RoomList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public Guid? OrderId { get; set; }

        public int? lineid { get; set; }

        [StringLength(50)]
        public string roomname { get; set; }

        public int? berth { get; set; }

        public int? peoples { get; set; }

        public int? adults { get; set; }

        public int? childs { get; set; }

        [StringLength(10)]
        public string roomcode { get; set; }

        [StringLength(10)]
        public string roomno { get; set; }

        public int? roomnoid { get; set; }

        [StringLength(50)]
        public string guestids { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AutoId { get; set; }

        [StringLength(30)]
        public string OrderName { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        public int? ordercompany { get; set; }

        public int? roomid { get; set; }

        public byte? OrderFlag { get; set; }

        public DateTime? OrderTime { get; set; }

        public int? orderdept { get; set; }

        public int? rebate { get; set; }

        [StringLength(1)]
        public string BedType { get; set; }

        [StringLength(1)]
        public string MergeFlag { get; set; }
    }
}
