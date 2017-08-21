namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_ManageUser
    {
        public Guid Id { get; set; }

        [StringLength(20)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string LoginPassWord { get; set; }

        public int? UserRight { get; set; }

        public int? UserDept { get; set; }
    }
}
