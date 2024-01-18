using System.ComponentModel.DataAnnotations;

namespace TestDemo1.Models
{
    public class StandardModel
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name length can't be more than 30.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string StudentName { get; set; }

        [Required]
        public string Standard { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        [Range(5, 18, ErrorMessage = "Age must be between 5 and 18.")]
        public string Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Address length can't be more than 20.")]
        public string Address { get; set; }
    }
}
