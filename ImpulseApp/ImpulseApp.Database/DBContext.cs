using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.StatModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Database
{
    class ImpulseContext:DbContext
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
    }
}
