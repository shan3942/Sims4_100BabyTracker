using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Sims4_100BabyTracker.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=BabyTracker") { }

        public virtual DbSet<Matriarch> Matriarches { get; set; }
        public virtual DbSet<Child> Children { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}