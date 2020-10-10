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
        [MaxLength(250, ErrorMessage="Maximum length for Snippet Description is 250 characters")]
        public string SnippetDescription { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage="Maximum length for Platform is 20 characters")]
        public string Platform { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage="Maximum length for Snippet is 250 characters")]
        public string Snippet { get; set; }

        public ICollection<CommandImage> Images {get;set;}
        
    }
}
