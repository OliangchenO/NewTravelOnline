namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_GuestRoomInfo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(20)]
        public string GuestName { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid OrderId { get; set; }

        public int? roomid { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AutoId { get; set; }

        public int? combineid { get; set; }

        public int? listid { get; set; }

        public int? LineID { get; set; }

        public int? DinnerId { get; set; }

        public int? PlanAllotid { get; set; }

        public int? PrintSort { get; set; }

        public string DinnerClaim { get; set; }

        [StringLength(30)]
        public string OrderName { get; set; }

        public byte? OrderNums { get; set; }

        [StringLength(20)]
        public string TabelNo { get; set; }

        [StringLength(50)]
        public string DinnerTime { get; set; }

        public int? PlanNo { get; set; }

        public int? RoomNoid { get; set; }

        [StringLength(30)]
        public string GuestEnName { get; set; }

        [StringLength(20)]
        public string IdNumber { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(11)]
        public string OrderMobile { get; set; }

        [StringLength(20)]
        public string OrderTel { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        public int? rankno { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        public DateTime? PassBgn { get; set; }

        public DateTime? PassEnd { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Sign { get; set; }

        [StringLength(20)]
        public string Home { get; set; }

        [StringLength(1)]
        public string IsLeader { get; set; }

        public int? visitcount { get; set; }

        [StringLength(100)]
        public string OrderMemo { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        [StringLength(20)]
        public string Vocation { get; set; }

        [StringLength(1)]
        public string visaflag { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string otherinfo { get; set; }

        public int? ranks { get; set; }

        [StringLength(10)]
        public string firstcj { get; set; }

        [StringLength(20)]
        public string cjdate { get; set; }

        [StringLength(20)]
        public string cjmdd { get; set; }

        [StringLength(50)]
        public string cjsy { get; set; }

        [StringLength(20)]
        public string TongXing { get; set; }

        [StringLength(50)]
        public string Company { get; set; }
    }
}
