using System.ComponentModel.DataAnnotations;

namespace TestDemo1.Models
{
    public class StandardModel
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string teacherName { get; set; }

        public string Standard { get; set; }

        public string Section { get; set; }

        public string Age { get; set; }
     
        public string Gender { get; set; }
      
        public string Address { get; set; }
    }
}
