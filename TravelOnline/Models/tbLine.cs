namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbLine
    {
        public int ID { get; set; }
        public Nullable<System.Guid> UID { get; set; }
        public Nullable<int> ErpID { get; set; }
        public Nullable<short> Types { get; set; }
        public Nullable<int> SetOut { get; set; }
        public string SetOutName { get; set; }
        public Nullable<short> LineType { get; set; }
        public Nullable<int> TravelType { get; set; }
        public Nullable<int> AirPlanID { get; set; }
        public string AirPlanName { get; set; }
        public Nullable<int> LineNameID { get; set; }
        public string LineName { get; set; }
        public Nullable<int> Plays { get; set; }
        public string PlaysName { get; set; }
        public Nullable<int> Topic { get; set; }
        public string TopicName { get; set; }
        public string Tags { get; set; }
        public string TagsName { get; set; }
        public string CusTags { get; set; }
        public string CusTagsName { get; set; }
        public Nullable<short> Days { get; set; }
        public Nullable<short> Nights { get; set; }
        public Nullable<byte> StopDays { get; set; }
        public string Describe { get; set; }
        public string Cname { get; set; }
        public string ShortPinYin { get; set; }
        public string PhotoPath { get; set; }
        public string Ensure { get; set; }
        public string EnsureName { get; set; }
        public Nullable<int> Price { get; set; }
        public string Traffic { get; set; }
        public Nullable<short> Brand { get; set; }
        public Nullable<byte> FreeTime { get; set; }
        public string Recomm { get; set; }
        public string Memo { get; set; }
        public string Destination { get; set; }
        public string DestName { get; set; }
        public Nullable<byte> BrowseType { get; set; }
        public string BrowseDept { get; set; }
        public Nullable<int> CreateComID { get; set; }
        public Nullable<int> CreateDeptID { get; set; }
        public string CreateDeptName { get; set; }
        public Nullable<int> CreateUserID { get; set; }
        public string CreateUserName { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<byte> UseFlag { get; set; }
        public string Schedule { get; set; }
        public Nullable<short> ScheduleCount { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<short> SalesTag { get; set; }
        public Nullable<int> ChildIntro { get; set; }
        public Nullable<short> MaxOrder { get; set; }
        public Nullable<byte> Multiple { get; set; }
        public Nullable<int> MinGather { get; set; }
        public Nullable<int> RelationID { get; set; }
        public Nullable<int> CruiseBoatID { get; set; }
        public string PageTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<bool> isIndex { get; set; }
        public Nullable<bool> isCommend { get; set; }
        public Nullable<bool> isBest { get; set; }
        public Nullable<bool> isTop { get; set; }
        public Nullable<bool> Enable { get; set; }
        public Nullable<System.DateTime> InputTime { get; set; }
        public string DestinationName { get; set; }
    }
}