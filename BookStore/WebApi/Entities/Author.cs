using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}