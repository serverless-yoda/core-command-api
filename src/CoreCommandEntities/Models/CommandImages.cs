using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommandEntities.Models {
    public class CommandImage {

        [Column("CommandImageId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage="Maximum length for Url is 100 characters")]
        public string Url { get; set; }
        public int OrderShown { get; set; } = 0;

        [ForeignKey(nameof(Command))]
        public Guid CommandId { get; set; }
        public Command Command {get;set;}
    }
}