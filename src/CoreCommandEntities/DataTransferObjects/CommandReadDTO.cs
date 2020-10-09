using System;
using System.Collections.Generic;
using CoreCommandEntities.Models;

namespace CoreCommandEntities.DTO {
     public class CommandReadDTO
    {
        public Guid Id { get; set; }  
        public string SnippetDescription { get; set; }
        public string Platform { get; set; }
        public string Snippet { get; set; }
        public ICollection<CommandImage> Images {get;set;}
        
    }
}