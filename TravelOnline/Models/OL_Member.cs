namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Member
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public DateTime? birthday { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(20)]
        public string mobile { get; set; }

        public short? level { get; set; }

        public int? integral { get; set; }

        public DateTime? joindate { get; set; }

        public byte? Sex { get; set; }

        [StringLength(100)]
        public string Address { get; set; }
    }
}
