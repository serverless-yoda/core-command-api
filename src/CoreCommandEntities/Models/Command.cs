using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommandEntities.Models
{
    public class Command
    {

        [Column("CommandId")]
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string SnippetDescription { get; set; }

        [Required]
        public string Platform { get; set; }
        [Required]
        public string Snippet { get; set; }

        public ICollection<CommandImage> Images {get;set;}
        
    }
}
