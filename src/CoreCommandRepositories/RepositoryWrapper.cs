using CoreCommandContracts;
using CoreCommandEntities.Data;

namespace CoreCommandRepositories {
    public class RepositoryWrapper : IRepositoryWrapper {
        private ICommandImageRepository CommandImageRepository;
        private ICommandRepository CommandRepository;
        private CoreCommandContext CoreCommandContext;
        public RepositoryWrapper(CoreCommandContext coreCommandContext)
        { 
            CoreCommandContext = coreCommandContext;
        }
        public ICommandRepository Command {

            get {
                if(CommandRepository == null) {
                    CommandRepository = new CommandRepository(CoreCommandContext);
                }
                return CommandRepository;
            }
        }
        public ICommandImageRepository CommandImage {

            get {
                if(CommandImageRepository == null) {
                    CommandImageRepository = new CommandImageRepository(CoreCommandContext);
                }
                return CommandImageRepository;
            }
        }
      
        public bool Save(){
            return CoreCommandContext.SaveChanges() >= 0;
        }
    }
}