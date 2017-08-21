namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_GuestInfo
    {
        public int id { get; set; }

        public Guid OrderId { get; set; }

        [StringLength(20)]
        public string GuestName { get; set; }

        [StringLength(30)]
        public string GuestEnName { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        public byte? IdType { get; set; }

        [StringLength(20)]
        public string IdNumber { get; set; }

        public DateTime? BirthDay { get; set; }

        [StringLength(1)]
        public string PassType { get; set; }

        public DateTime? PassBgn { get; set; }

        public DateTime? PassEnd { get; set; }

        [StringLength(20)]
        public string Sign { get; set; }

        [StringLength(20)]
        public string Home { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        public int? allotid { get; set; }

        public int? roomid { get; set; }

        public int? listid { get; set; }

        public int? rankno { get; set; }

        public string visitid { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        [StringLength(20)]
        public string IdentityCard { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string EAddress { get; set; }

        [StringLength(50)]
        public string ESign { get; set; }

        public int? DinnerId { get; set; }

        public int? PlanAllotid { get; set; }

        public int? PrintSort { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(1)]
        public string IsLeader { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(20)]
        public string Vocation { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        [StringLength(20)]
        public string TongXing { get; set; }

        [StringLength(10)]
        public string firstcj { get; set; }

        [StringLength(20)]
        public string cjdate { get; set; }

        [StringLength(20)]
        public string cjmdd { get; set; }

        [StringLength(50)]
        public string cjsy { get; set; }

        [StringLength(1)]
        public string visaflag { get; set; }

        [StringLength(50)]
        public string otherinfo { get; set; }

        public int? ranks { get; set; }
    }
}
