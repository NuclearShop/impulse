using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.StatModels;
using ImpulseApp.Models.UtilModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Database
{
    class ImpulseContext : DbContext
    {

        public ImpulseContext()
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<SimpleAdModel> SimpleAds { get; set; }
        public DbSet<Click> ClickStats { get; set; }
        public DbSet<AdSession> Sessions { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<AdState> AdStates { get; set; }
        public DbSet<VideoUnit> VideoUnits { get; set; }
        public DbSet<NodeLink> NodeLinks { get; set; }
        public DbSet<UserElement> UserElements { get; set; }
        public DbSet<ModeratorView> ModeratorViews { get; set; }
        public DbSet<HtmlTag> HtmlTags { get; set; }
        public DbSet<Versioning> Versioning { get; set; }
        public DbSet<ABTest> AbTests { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<ABTest>()
        .HasOptional(a => a.AdA)
        .WithMany()
        .WillCascadeOnDelete(false);
            modelBuilder.Entity<ABTest>()
        .HasOptional(a => a.AdB)
        .WithMany()
        .WillCascadeOnDelete(false);
           /* modelBuilder.Entity<SimpleAdModel>()
        .HasOptional(a => a.AdStates)
        .WithOptionalDependent()
        .WillCascadeOnDelete(true);
            modelBuilder.Entity<AdState>()
       .HasOptional(a => a.UserElements)
       .WithOptionalDependent()
       .WillCascadeOnDelete(true);
            modelBuilder.Entity<AdSession>()
        .HasOptional(a => a.Activities)
        .WithOptionalDependent()
        .WillCascadeOnDelete(true);
            modelBuilder.Entity<Activity>()
        .HasOptional(a => a.Clicks)
        .WithOptionalDependent()
        .WillCascadeOnDelete(true);*/
        }
    }
}
