namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_LoginUser
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(32)]
        public string LoginPassWord { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string deptname { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(11)]
        public string Mobile { get; set; }

        public byte? Sex { get; set; }

        public DateTime? BirtyDay { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(6)]
        public string ZipCode { get; set; }

        public byte? Marriage { get; set; }

        public byte? Income { get; set; }

        public byte? Vip { get; set; }

        [StringLength(100)]
        public string Hobby { get; set; }

        public byte? MobVerify { get; set; }

        public byte? Career { get; set; }

        public int? LoginCount { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        public byte? IdType { get; set; }

        [StringLength(20)]
        public string IdNumber { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        public DateTime? RegTime { get; set; }

        [StringLength(20)]
        public string LoginIp { get; set; }

        public int? OldId { get; set; }

        public int? deptid { get; set; }

        public int? companyid { get; set; }

        public int? misid { get; set; }
    }
}
