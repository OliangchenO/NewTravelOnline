namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_ProductClass
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [StringLength(20)]
        public string ProductName { get; set; }

        [StringLength(100)]
        public string ProductUrl { get; set; }

        public byte? ProductSort { get; set; }

        public int? MisClassId { get; set; }

        [StringLength(50)]
        public string ClassPath { get; set; }

        [StringLength(300)]
        public string ClassList { get; set; }

        public byte? ClassLevel { get; set; }

        [StringLength(50)]
        public string ProductType { get; set; }
    }
}
