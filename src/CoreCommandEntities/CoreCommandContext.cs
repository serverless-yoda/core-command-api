using Microsoft.EntityFrameworkCore;
using CoreCommandEntities.Models;

namespace CoreCommandEntities.Data {
    public class CoreCommandContext: DbContext {
        public CoreCommandContext(DbContextOptions<CoreCommandContext> options): base(options)
        {
            
        }

        public DbSet<CommandImage> CommandImages {get;set;}
        public DbSet<Command> Commands {get;set;}
    }
}