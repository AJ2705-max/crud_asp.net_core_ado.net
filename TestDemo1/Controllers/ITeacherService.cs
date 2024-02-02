using System.Reflection.Metadata.Ecma335;
using TestDemo1.Controllers;
using TestDemo1.Models;

namespace TestDemo1.Controllers
{
    public interface ITeacherService
    {

        public string StudentName { get; set; }
        public string teacherName { get; set; }
        public string Standard { get; set; }
        public string Section { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        public  void AddStandard(StandardController standard);
        public dynamic GetTeacherNames();
        public void AddStandard(ITeacherService teacherService);
        public void AddStandard(StandardModel standard);
        
    }
}