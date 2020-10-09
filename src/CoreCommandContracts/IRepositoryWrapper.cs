namespace CoreCommandContracts {
    public interface IRepositoryWrapper {
        ICommandRepository Command {get;}
        ICommandImageRepository CommandImage {get;}
        bool Save();
    }
}