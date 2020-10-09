using System;
using System.ComponentModel.DataAnnotations;

   namespace CoreCommandEntities.DTO {
     public class CommandCreateDTO
    {       
        [Required]
        [MaxLength(250)]
        public string SnippetDescription { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public string Snippet { get; set; }    
    }
}
