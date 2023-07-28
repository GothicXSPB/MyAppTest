using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyApp.Models
{
    public class Employees
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        
        [MaxLength(50)]
        [System.ComponentModel.DataAnnotations.Schema.Index]
        public string? LastName { get; set; }

        [MaxLength(50)]
        public string? Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(50)]
        [System.ComponentModel.DataAnnotations.Schema.Index]
        public string? Gender { get; set; }

        public int? Age { get; set; }
    }
}
