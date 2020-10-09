using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommandEntities.Models {
    public class CommandImage {

        [Column("CommandImageId")]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public int OrderShown { get; set; }

        [ForeignKey(nameof(Command))]
        public Guid CommandId { get; set; }
        public Command Command {get;set;}
    }
}