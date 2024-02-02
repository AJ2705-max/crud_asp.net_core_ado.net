using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.Json;
using TestDemo1.Models;


namespace TestDemo1.Controllers
{
    public class StandardController : Controller
    {
        private readonly IConfiguration _configuration;
        private string connectionString;
       //private readonly TeacherController _teacherController;
        private readonly ITeacherService _teacherService;
        public object teacherName;

        //public object StudentName { get; internal set; }
        //public object TeacherName { get; internal set; }
        //public object Standard { get; internal set; }
        //public object Section { get; internal set; }
        //public object Age { get; internal set; }
        //public object Gender { get; internal set; }
        //public object Address { get; internal set; }

        public StandardController(IConfiguration configuration,ITeacherService teacherService)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
           // _teacherController = teacherController;
            _teacherService = teacherService; 
        }

        public IActionResult Create()
        {
            ViewBag.TeacherList = _teacherService.GetTeacherNames();
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

        public IActionResult ViewStandard()
        {
            List<StandardModel> standardlist = new List<StandardModel>();

            const string selectQuery = "SELECT * FROM StudentStandard;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StandardModel standard = new StandardModel();
                       // standard.StudentId = (int)reader["StudentId"];
                        standard.StudentName = reader["StudentName"].ToString();
                        standard.teacherName = reader["teacherName"].ToString();
                        standard.Standard = reader["Standard"].ToString();
                        standard.Section = reader["Section"].ToString();
                        standard.Age = reader["Age"].ToString();
                        standard.Gender = reader["Gender"].ToString();
                        standard.Address = reader["Address"].ToString();

                        standardlist.Add(standard);

                    }
                }
            }
            return View(standardlist);
        }

        public IActionResult AddStandard()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult AddStandard([FromBody] StandardModel standard, ITeacherService teacherService)
        //{
        //    teacherService.AddStandard(standard);

        //    string teacherName = teacherService.GetTeacherNames();

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        const string query = "INSERT INTO StudentStandard (StudentName, teacherName, Standard, Section, Age, Gender, Address) VALUES (@StudentName, @teacherName, @Standard, @Section, @Age, @Gender, @Address);";

        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            connection.Open();

        //            command.Parameters.AddWithValue("@StudentName", standard.StudentName);
        //            command.Parameters.AddWithValue("@teacherName", teacherName);
        //            command.Parameters.AddWithValue("@Standard", standard.Standard);
        //            command.Parameters.AddWithValue("@Section", standard.Section);
        //            command.Parameters.AddWithValue("@Age", standard.Age);
        //            command.Parameters.AddWithValue("@Gender", standard.Gender);
        //            command.Parameters.AddWithValue("@Address", standard.Address);

        //            command.ExecuteNonQuery();
        //        }
        //    }


        //    return Json(new { success = true, message = "Data updated successfully" });
        //}
        //[HttpPost]  <<--Not Working-->>
        //public IActionResult AddStandard([FromBody] StandardModel standard)   
        //{
        //    // Fetch the teacher names
        //    ViewBag.TeacherList = _teacherController.GetTeacherNames();

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        const string query = "INSERT INTO StudentStandard (StudentName, teacherName ,Standard, Section, Age, Gender, Address) values (@StudentName, @teacherName, @Standard, @Section, @Age,  @Gender, @Address);";

        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            connection.Open();

        //            command.Parameters.AddWithValue("@StudentName", standard.StudentName);
        //            command.Parameters.AddWithValue("@teacherName", standard.teacherName);
        //            command.Parameters.AddWithValue("@Standard", standard.Standard);
        //            command.Parameters.AddWithValue("@Section", standard.Section);
        //            command.Parameters.AddWithValue("@Age", standard.Age);
        //            command.Parameters.AddWithValue("@Gender", standard.Gender);
        //            command.Parameters.AddWithValue("@Address", standard.Address);

        //            command.ExecuteNonQuery();
        //        }
        //    }

        //    // If you want to use the ViewBag data in the view returned after adding a standard,
        //    // you should return a ViewResult instead of a JsonResult.
        //    // Replace "ViewStandard" with the name of your view.
        //    return View("ViewStandard");
        //}


        //[HttpPost]
        //public IActionResult AddStandard([FromBody] StandardModel standard)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        const string query = "INSERT INTO StudentStandard (StudentName, Standard, Section, Age, Gender, Address) values (@StudentName, @Standard, @Section, @Age,  @Gender, @Address);";

        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            connection.Open();

        //            command.Parameters.AddWithValue("@StudentName", standard.StudentName);
        //            command.Parameters.AddWithValue("@Standard", standard.Standard);
        //            command.Parameters.AddWithValue("@Section", standard.Section);
        //            command.Parameters.AddWithValue("@Age", standard.Age);
        //            command.Parameters.AddWithValue("@Gender", standard.Gender);
        //            command.Parameters.AddWithValue("@Address", standard.Address);

        //            command.ExecuteNonQuery();
        //        }
        //    }

        //    // Redirect to the TeacherController with the selected standard as a query string parameter
        //    return RedirectToAction("AddTeacher", "Teacher", new { standard = standard.Standard });
        //}

        [HttpPost]
        public IActionResult AddStandard( StandardModel standardModel)
        {

            //StandardModel standards = JsonSerializer.Deserialize<StandardModel>(standard)!;

            standardModel.teacherName = _teacherService.GetTeacherNames();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                const string query = "INSERT INTO StudentStandard (StudentName, teacherName, Standard, Section, Age, Gender, Address) VALUES (@StudentName, @teacherName ,@Standard, @Section, @Age,  @Gender, @Address);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@StudentName", standardModel.StudentName);
                    command.Parameters.AddWithValue("@teacherName", teacherName);
                    command.Parameters.AddWithValue("@Standard", standardModel.Standard);
                    command.Parameters.AddWithValue("@Section", standardModel.Section);
                    command.Parameters.AddWithValue("@Age", standardModel.Age);
                    command.Parameters.AddWithValue("@Gender", standardModel.Gender);
                    command.Parameters.AddWithValue("@Address", standardModel.Address);

                    command.ExecuteNonQuery();
                }
            }
            return Json("ViewStandard");
        }

        [HttpGet]
        public IActionResult PopulateUpdateStandard(int studentid)
        {
            StandardModel standardModel = null;

            const string queryString = "SELECT * FROM StudentStandard WHERE StudentId = @studentid";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@studentid", studentid);

                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        standardModel = new StandardModel();

                        //standardModel.StudentId = (int)reader["StudentId"];
                        standardModel.StudentName = reader["StudentName"].ToString();
                        standardModel.Standard =reader["Standard"].ToString();
                        standardModel.Section = reader["Section"].ToString();
                        standardModel.Age = reader["Age"].ToString();
                        standardModel.Gender = reader["Gender"].ToString();
                        standardModel.Address = reader["Address"].ToString();
                    }
                }
            }

            return Json(standardModel);
        }

        [HttpPost]
        public IActionResult UpdateStandard(int Id, string model)
        {
            StandardModel studentmodel = JsonSerializer.Deserialize< StandardModel >(model)!;

          //  studentmodel.StudentId = Id;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string queryString = "UPDATE StudentStandard SET StudentName = @StudentName, Standard = @Standard, Section = @Section, Age = @Age, Gender = @Gender, Address = @Address WHERE StudentId = @StudentId;";

                using (MySqlCommand command = new MySqlCommand(queryString, connection))
                {
                    //command.Parameters.AddWithValue("@StudentId", studentmodel.StudentId);
                    command.Parameters.AddWithValue("@StudentName", studentmodel.StudentName);
                    command.Parameters.AddWithValue("@Standard", studentmodel.Standard);
                    command.Parameters.AddWithValue("@Section", studentmodel.Section);
                    command.Parameters.AddWithValue("@Age", studentmodel.Age);
                    command.Parameters.AddWithValue("@Gender", studentmodel.Gender);
                    command.Parameters.AddWithValue("@Address", studentmodel.Address);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            return Json("ViewStudents");
        }

        [HttpPost]
        public IActionResult DeleteStandard(int studentid) 
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString)) 
            {
                string queryString = "DELETE FROM StudentStandard WHERE StudentId = @StudentId;";

                using (MySqlCommand command = new MySqlCommand(queryString, connection)) 
                {
                    command.Parameters.AddWithValue("@StudentId", studentid);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("ViewStandard");
        }


    }
}
