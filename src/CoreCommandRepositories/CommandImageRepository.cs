using CoreCommandContracts;
using CoreCommandEntities.Models;
using CoreCommandEntities.Data;

namespace CoreCommandRepositories { 
    public class CommandImageRepository: BaseRepository<CommandImage>, ICommandImageRepository {

        public CommandImageRepository(CoreCommandContext repoContext):base(repoContext)
        {
            
        }
    }
}