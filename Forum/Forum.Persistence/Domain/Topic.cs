using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Persistence.Domain
{
    public class Topic : Entidad
    {
        [Required,MaxLength((50),ErrorMessage = "El {0} no puede superar los {1} caracteres")]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public List<String> Tags { get; set; }
        public Author Author { get; set; }
    }
}
