using CoreCommandContracts;
using CoreCommandEntities.Models;
using CoreCommandEntities.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace CoreCommandRepositories {
    public class CommandRepository 
    : BaseRepository<Command>, ICommandRepository 
    {
        public CommandRepository(CoreCommandContext repoContext)
        :base(repoContext){ }

        public IEnumerable<Command> GetAllCommands() {
            return GetAll(false).OrderBy(c => c.Platform).ToList();
        }
    }
}