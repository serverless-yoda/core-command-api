using Microsoft.EntityFrameworkCore;
using CoreCommandEntities.Models;
using CoreCommandEntities.SeedConfiguration;

namespace CoreCommandEntities.Data {
    public class CoreCommandContext: DbContext {
        public CoreCommandContext(DbContextOptions<CoreCommandContext> options)
        : base(options)
        {
            
        }

        public DbSet<CommandImage> CommandImages {get;set;}
        public DbSet<Command> Commands {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new CommandConfiguration());
            modelBuilder.ApplyConfiguration(new CommandImageConfiguration());            
        }
    }
}