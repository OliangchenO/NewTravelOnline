using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TravelOnline.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("name=SCYTSModel") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Database.SetInitializer(new DropCreateDatabaseAlways<MyDbContext>());
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }
        public DbSet<OL_FlashAD> OL_FlashAD { get; set; }
        public DbSet<SpecialTopic> SpecialTopic { get; set; }
        public DbSet<View_SpecialLine_New> View_SpecialLine_New { get; set; }
        public DbSet<View_SpecialLineTemp_New> View_SpecialLineTemp_New { get; set; }
    }
}