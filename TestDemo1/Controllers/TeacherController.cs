using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using TestDemo1.Models;

namespace TestDemo1.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly TeacherController _teacherController;
        private string connectionString;
        public TeacherController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            
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



        //public IActionResult ViewTeacher() <<---Not Working-->>
        //{
        //    List<TeacherModel> teacherlist = new List<TeacherModel>();

        //    const string selectQuery = "SELECT * FROM teacher;";

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
        //        {
        //            connection.Open();

        //            MySqlDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                TeacherModel teacher = new TeacherModel();
        //                teacher.id = (int)reader["id"];
        //                teacher.teacherName = (string)reader["teacherName"];
        //                teacher.Standard = (string)reader["Standard"];

        //                teacherlist.Add(teacher);
        //            }
        //        }
        //    }
        //    return View(teacherlist);
        //}

        public IActionResult AddStandard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTeacher([FromBody] TeacherModel teacher, string standard)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                const string query = "INSERT INTO Teacher (id, teacherName, Standard) values (@id, @teacherName, @Standard);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@id", teacher.id);
                    command.Parameters.AddWithValue("@teacherName", teacher.teacherName);
                    command.Parameters.AddWithValue("@Standard", teacher.Standard); // Use the standard parameter here

                    command.ExecuteNonQuery();
                }
            }
            return Json("ViewTeacher");
        }

        //[HttpPost]
        //public IActionResult AddTeacher([FromBody] TeacherModel teacher, string Standard)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        const string query = "INSERT INTO Teacher (id, name, Standard) values (@id, @name, @Standard);";

        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            connection.Open();

        //            command.Parameters.AddWithValue("@id", teacher.id);
        //            command.Parameters.AddWithValue("@name", teacher.name);
        //          //  command.Parameters.AddWithValue("@Standard", teacher.Standard);

        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    return Json("ViewTeacher");
        //}

        //    [HttpGet]
        //    public ActionResult GetStudentStandard()
        //    {
        //        const string query = "CALL GetStudentStandard()";
        //        var model = new List<TeacherModel>();

        //        using (MySqlConnection conn = new MySqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            using (MySqlCommand cmd = new MySqlCommand(query, conn))
        //            {
        //                using (var reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var studentStandard = new TeacherModel
        //                        {
        //                            StudentId = Convert.ToInt32(reader["StudentId"]),
        //                            Standard = reader["Standard"].ToString()
        //                        };
        //                        model.Add(studentStandard);
        //                    }
        //                }
        //            }
        //        }

        //        return View(model);
        //    }
        //}



    }
}

