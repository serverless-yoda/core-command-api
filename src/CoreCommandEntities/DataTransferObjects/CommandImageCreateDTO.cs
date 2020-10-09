using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommandEntities.DTO {
    public class CommandImageCreateDTO {
      
        public string Url { get; set; }
        public int OrderShown { get; set; }
        //public Guid CommandId {get;set;}
      
    }
}