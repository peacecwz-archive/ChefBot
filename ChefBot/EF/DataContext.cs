using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChefBot.EF
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DbConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Tables.RecipeEntity> Recipes { get; set; }
        public DbSet<Tables.BotHistoryEntity> BotHistories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tables.RecipeEntity>()
                .ToTable("Recipes");

            modelBuilder.Entity<Tables.BotHistoryEntity>()
                .ToTable("BotHistories");
        }
    }
}