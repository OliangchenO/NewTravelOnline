namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Order
    {
        public Guid OrderId { get; set; }

        [StringLength(10)]
        public string ProductType { get; set; }

        public int? ProductClass { get; set; }

        public int? LineID { get; set; }

        public int? PlanId { get; set; }

        [StringLength(100)]
        public string LineName { get; set; }

        public DateTime? BeginDate { get; set; }

        public byte? OrderNums { get; set; }

        public byte? Adults { get; set; }

        public byte? Childs { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [StringLength(30)]
        public string OrderName { get; set; }

        [StringLength(50)]
        public string OrderEmail { get; set; }

        [StringLength(11)]
        public string OrderMobile { get; set; }

        [StringLength(20)]
        public string OrderTel { get; set; }

        [StringLength(20)]
        public string OrderFax { get; set; }

        [StringLength(100)]
        public string OrderMemo { get; set; }

        public DateTime? OrderTime { get; set; }

        public Guid? OrderUser { get; set; }

        public int? DeptId { get; set; }

        public byte? OrderFlag { get; set; }

        public byte? Contract { get; set; }

        public byte? Invoice { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AutoId { get; set; }

        public byte? LineDays { get; set; }

        public byte? PayFlag { get; set; }

        [StringLength(1)]
        public string RouteFlag { get; set; }

        [StringLength(50)]
        public string PlanNo { get; set; }

        [StringLength(1)]
        public string PayType { get; set; }

        public int? BranchId { get; set; }

        public int? shipid { get; set; }

        public int? orderdept { get; set; }

        public int? ordercompany { get; set; }

        public int? ProductNum { get; set; }

        public int? rebate { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        public int? ccid { get; set; }

        [StringLength(1)]
        public string RebateFlag { get; set; }

        public int? allmdjs { get; set; }

        [StringLength(50)]
        public string ota { get; set; }
    }
}
