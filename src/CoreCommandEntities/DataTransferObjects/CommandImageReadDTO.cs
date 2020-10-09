using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommandEntities.DTO {
    public class CommandImageReadDTO {

    
        public Guid Id { get; set; }
        public string Url { get; set; }
        public int OrderShown { get; set; }

      
    }
}