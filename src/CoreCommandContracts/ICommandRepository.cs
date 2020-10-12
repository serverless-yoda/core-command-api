using System.Collections.Generic;
using CoreCommandEntities.Models;

namespace CoreCommandContracts
{
    public interface ICommandRepository: 
    IBaseRepository<Command>
    {
         IEnumerable<Command> GetAllCommands();
    }
}