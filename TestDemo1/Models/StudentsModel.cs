using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDemo1.Models
{
    [Table("students")]
    public class StudentsModel
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Name length can't be more than 10.")]
        public string StudentName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Address length can't be more than 20.")]
        public string StudentAddress { get; set; }

        [Required]
        [Range(5, 18, ErrorMessage = "Age must be between 5 and 18.")]
        public int StudentAge { get; set; }
    }
}
