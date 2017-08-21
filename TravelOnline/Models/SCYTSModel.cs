namespace TravelOnline.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SCYTSModel : DbContext
    {
        public SCYTSModel()
            : base("name=SCYTSModel")
        {
        }

        public virtual DbSet<Act_ActInfoMain> Act_ActInfoMain { get; set; }
        public virtual DbSet<Act_Order> Act_Order { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CR_BusNo> CR_BusNo { get; set; }
        public virtual DbSet<CR_Combine> CR_Combine { get; set; }
        public virtual DbSet<CR_Company> CR_Company { get; set; }
        public virtual DbSet<CR_Confirm> CR_Confirm { get; set; }
        public virtual DbSet<CR_Damage> CR_Damage { get; set; }
        public virtual DbSet<CR_DinnerNo> CR_DinnerNo { get; set; }
        public virtual DbSet<CR_MisDoError> CR_MisDoError { get; set; }
        public virtual DbSet<CR_Pic> CR_Pic { get; set; }
        public virtual DbSet<CR_PlanNo> CR_PlanNo { get; set; }
        public virtual DbSet<CR_Rebate> CR_Rebate { get; set; }
        public virtual DbSet<CR_RoomAllot> CR_RoomAllot { get; set; }
        public virtual DbSet<CR_RoomList> CR_RoomList { get; set; }
        public virtual DbSet<CR_RoomNo> CR_RoomNo { get; set; }
        public virtual DbSet<CR_RoomOrder> CR_RoomOrder { get; set; }
        public virtual DbSet<CR_Route> CR_Route { get; set; }
        public virtual DbSet<CR_Ship> CR_Ship { get; set; }
        public virtual DbSet<CR_ShipRoom> CR_ShipRoom { get; set; }
        public virtual DbSet<CR_Visit> CR_Visit { get; set; }
        public virtual DbSet<CR_VisitList> CR_VisitList { get; set; }
        public virtual DbSet<CTY_FuJia> CTY_FuJia { get; set; }
        public virtual DbSet<DeptInfo> DeptInfo { get; set; }
        public virtual DbSet<fanli> fanli { get; set; }
        public virtual DbSet<ICBC_Pay> ICBC_Pay { get; set; }
        public virtual DbSet<InitData> InitData { get; set; }
        public virtual DbSet<LineDest> LineDest { get; set; }
        public virtual DbSet<OL_Affiche> OL_Affiche { get; set; }
        public virtual DbSet<OL_Appraise> OL_Appraise { get; set; }
        //public virtual DbSet<OL_CuisesRoom> OL_CuisesRoom { get; set; }
        public virtual DbSet<OL_Dept> OL_Dept { get; set; }
        public virtual DbSet<OL_Destination> OL_Destination { get; set; }
        public virtual DbSet<OL_Favorite> OL_Favorite { get; set; }
        public virtual DbSet<OL_FlashAD> OL_FlashAD { get; set; }
        public virtual DbSet<OL_FriendLink> OL_FriendLink { get; set; }
        public virtual DbSet<OL_GuestInfo> OL_GuestInfo { get; set; }
        public virtual DbSet<OL_Integral> OL_Integral { get; set; }
        public virtual DbSet<OL_Invoice> OL_Invoice { get; set; }
        public virtual DbSet<OL_Journal> OL_Journal { get; set; }
        public virtual DbSet<OL_Line> OL_Line { get; set; }
        public virtual DbSet<tbLine> tbLine { get; set; }
        public virtual DbSet<OL_LoginUser> OL_LoginUser { get; set; }
        public virtual DbSet<OL_ManageUser> OL_ManageUser { get; set; }
        public virtual DbSet<OL_Member> OL_Member { get; set; }
        public virtual DbSet<OL_Order> OL_Order { get; set; }
        public virtual DbSet<OL_OrderApply> OL_OrderApply { get; set; }
        //public virtual DbSet<OL_OrderExtend> OL_OrderExtend { get; set; }
        //public virtual DbSet<OL_OrderPrice> OL_OrderPrice { get; set; }
        //public virtual DbSet<OL_OrderLog> OL_OrderLog { get; set; }
        public virtual DbSet<OL_Plan> OL_Plan { get; set; }
        public virtual DbSet<OL_ProductClass> OL_ProductClass { get; set; }
        public virtual DbSet<OL_ProductType> OL_ProductType { get; set; }
        public virtual DbSet<OL_SmsSend> OL_SmsSend { get; set; }
        public virtual DbSet<OL_Summary> OL_Summary { get; set; }
        public virtual DbSet<OL_TempOrder> OL_TempOrder { get; set; }
        public virtual DbSet<OL_UserRight> OL_UserRight { get; set; }
        public virtual DbSet<OL_View> OL_View { get; set; }
        public virtual DbSet<OL_ViewPic> OL_ViewPic { get; set; }
        public virtual DbSet<Ota_Cancel> Ota_Cancel { get; set; }
        public virtual DbSet<Ota_Guest> Ota_Guest { get; set; }
        public virtual DbSet<Ota_Order> Ota_Order { get; set; }
        public virtual DbSet<Ota_Pay> Ota_Pay { get; set; }
        public virtual DbSet<Ota_Price> Ota_Price { get; set; }
        public virtual DbSet<Pre_Policy> Pre_Policy { get; set; }
        public virtual DbSet<Pre_Ticket> Pre_Ticket { get; set; }
        public virtual DbSet<SeoLink> SeoLink { get; set; }
        public virtual DbSet<SpecialLine> SpecialLine { get; set; }
        public virtual DbSet<SpecialTopic> SpecialTopic { get; set; }
        public virtual DbSet<wz_dingzhi> wz_dingzhi { get; set; }
        public virtual DbSet<XiRongOrder> XiRongOrder { get; set; }
        public virtual DbSet<OL_JournalImg> OL_JournalImg { get; set; }
        public virtual DbSet<View_Act_GuestInfo> View_Act_GuestInfo { get; set; }
        public virtual DbSet<View_Act_GuestInfo_a> View_Act_GuestInfo_a { get; set; }
        public virtual DbSet<View_CR_BusNo> View_CR_BusNo { get; set; }
        public virtual DbSet<View_CR_DinnerNo> View_CR_DinnerNo { get; set; }
        public virtual DbSet<View_CR_PlanNo> View_CR_PlanNo { get; set; }
        public virtual DbSet<View_CR_RoomAllot> View_CR_RoomAllot { get; set; }
        public virtual DbSet<View_CR_RoomList> View_CR_RoomList { get; set; }
        public virtual DbSet<View_CR_Visit> View_CR_Visit { get; set; }
        public virtual DbSet<View_CR_VisitList> View_CR_VisitList { get; set; }
        public virtual DbSet<View_Destination> View_Destination { get; set; }
        public virtual DbSet<View_GuestRoomInfo> View_GuestRoomInfo { get; set; }
        public virtual DbSet<View_MemberOrder> View_MemberOrder { get; set; }
        public virtual DbSet<View_OL_View> View_OL_View { get; set; }
        public virtual DbSet<View_Prefer> View_Prefer { get; set; }
        public virtual DbSet<View_RoomOrderList> View_RoomOrderList { get; set; }
        public virtual DbSet<View_SpecialLine> View_SpecialLine { get; set; }
        public virtual DbSet<View_SpecialLine_New> View_SpecialLine_New { get; set; }
        public virtual DbSet<View_SpecialLineTemp> View_SpecialLineTemp { get; set; }
        public virtual DbSet<View_SpecialLineTemp_New> View_SpecialLineTemp_New { get; set; }
        public virtual DbSet<View_ThirdRoomCheck> View_ThirdRoomCheck { get; set; }
        public virtual DbSet<View_VisitReport> View_VisitReport { get; set; }
        public virtual DbSet<View_WeekSellCount> View_WeekSellCount { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Act_ActInfoMain>()
                .Property(e => e.ActivityName)
                .IsUnicode(false);

            modelBuilder.Entity<Act_ActInfoMain>()
                .Property(e => e.Start)
                .IsUnicode(false);

            modelBuilder.Entity<Act_ActInfoMain>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Act_ActInfoMain>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<Act_ActInfoMain>()
                .Property(e => e.Remark01)
                .IsUnicode(false);

            modelBuilder.Entity<Act_ActInfoMain>()
                .Property(e => e.Remark02)
                .IsUnicode(false);

            modelBuilder.Entity<Act_ActInfoMain>()
                .Property(e => e.ActivityRunSTime)
                .IsUnicode(false);

            modelBuilder.Entity<Act_ActInfoMain>()
                .Property(e => e.ActivityRunETime)
                .IsUnicode(false);

            modelBuilder.Entity<Act_Order>()
                .Property(e => e.OrderMobile)
                .IsUnicode(false);

            modelBuilder.Entity<OL_Integral>()
                .Property(e => e.flag)
                .IsUnicode(false);

            modelBuilder.Entity<OL_LoginUser>()
                .Property(e => e.LoginPassWord)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OL_ManageUser>()
                .Property(e => e.LoginPassWord)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<View_Act_GuestInfo>()
                .Property(e => e.Age)
                .HasPrecision(18, 0);

            modelBuilder.Entity<View_Act_GuestInfo_a>()
                .Property(e => e.Age)
                .HasPrecision(18, 0);

            modelBuilder.Entity<View_OL_View>()
                .Property(e => e.mapx)
                .HasPrecision(19, 6);

            modelBuilder.Entity<View_OL_View>()
                .Property(e => e.mapy)
                .HasPrecision(19, 6);

            modelBuilder.Entity<View_Prefer>()
                .Property(e => e.birtyday)
                .IsUnicode(false);

            modelBuilder.Entity<View_Prefer>()
                .Property(e => e.price)
                .HasPrecision(38, 2);

            modelBuilder.Entity<View_SpecialLine>()
                .Property(e => e.PlanTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<View_SpecialLineTemp>()
                .Property(e => e.PlanTypeName)
                .IsUnicode(false);
        }
    }
}
