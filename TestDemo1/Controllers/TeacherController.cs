using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using TestDemo1.Models;

namespace TestDemo1.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITeacherService _teacherService;
        private string connectionString;
      
        
        public TeacherController( IConfiguration configuration, ITeacherService teacherService)
        {
            teacherService = _teacherService;
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            
        }

        public IActionResult Create(ITeacherService teacherService)
        {
            teacherService.AddStandard(teacherService);
            ViewBag.TeacherList = teacherService.GetTeacherNames();
            return View();
        }

        

        public List<string> GetTeacherNames()
        {
            List<string> teacherNames = new List<string>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("GetAllTeacherNames", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    teacherNames.Add(rdr["teacherName"].ToString());
                }
            }

            return teacherNames;
        }




        public IActionResult ViewTeacher()
        {
            const string selectQuery = "SELECT * FROM teacher;";
            List<TeacherModel> teacherlist = new List<TeacherModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TeacherModel teacher = new TeacherModel();
                        teacher.id = (int)reader["id"];
                        teacher.teacherName = (string)reader["teacherName"];
                        teacher.Standard = (string)reader["Standard"];

                        teacherlist.Add(teacher);
                    }
                }
            }

            return View(teacherlist);
        }



    

        public IActionResult AddStandard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTeacher([FromBody] TeacherModel teacher, StandardModel standard)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                const string query = "INSERT INTO Teacher (id, teacherName, Standard) values (@id, @teacherName, @Standard);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@id", teacher.id);
                    command.Parameters.AddWithValue("@teacherName", teacher.teacherName);
                    command.Parameters.AddWithValue("@Standard", teacher.Standard); 

                    command.ExecuteNonQuery();
                }
            }
            return Json("ViewTeacher");
        }

        //[HttpGet]
        //public JsonResult GetStudentDetails(/*string StudentName ,*/ int teacherName)
        //{
        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand("GetStudentDetails", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add(new MySqlParameter("@teacherName", teacherName));
        //           // cmd.Parameters.Add(new MySqlParameter("@StudentName", StudentName));

        //            conn.Open();

        //            MySqlDataReader reader = cmd.ExecuteReader();

        //            var model = new List<string>();
        //            while (reader.Read())
        //            {
        //                model.Add(reader.GetString(0)); // Assuming StudentName is the first column
        //            }

        //            return Json(model);
        //        }
        //    }
        //}

        //[HttpGet]
        //public ActionResult GetStudentDetails(string StudentName)
        //{
        //    List<string> students = new List<string>();

        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand("GetStudentDetails", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@TeacherName", StudentName);

        //            conn.Open();

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    students.Add(reader.GetString(0));
        //                }
        //            }

        //            conn.Close();
        //        }
        //    }


        //    return Json(students);
        //}


        //[HttpGet]
        //public ActionResult GetStudentDetails(string StudentName)
        //{
        //    // Assuming you have a method to get student details
        //    var students = studentList(StudentName);

        //    // Return the data as JSON
        //    return Json(students);
        //}
        //[HttpGet]
        //public List<string> studentList( string teacherName)
        //{
        //    List<string> students = new List<string>();

        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand("GetStudentDetails", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("teacherName", teacherName);

        //            conn.Open();

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    students.Add(reader.GetString(0));
        //                }
        //            }

        //            conn.Close();
        //        }
        //    }

        //    return students;
        //}
        [HttpGet]
        public IActionResult GetStudentDetails(string teacherName)
        {
           const string selectquery = "SELECT StudentName from StudentStandard WHERE teacherName = teacherName;";

            List<StandardModel> studentList = new List<StandardModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectquery, connection))
                {
                    command.Parameters.AddWithValue("@teacherName", teacherName);
                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StandardModel standardModel = new StandardModel();
                        standardModel.StudentName = (string)reader["StudentName"];

                        studentList.Add(standardModel);
                    }
                }
            }
            return Json(studentList);
        }
    }
}

