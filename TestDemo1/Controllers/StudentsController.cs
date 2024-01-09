﻿using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TestDemo1.Models;

namespace TestDemo1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public StudentsController(IConfiguration configuration) 
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult ViewStudents()
        {
            List<StudentsModel> studentList = new List<StudentsModel>();

            const string selectQuery = "SELECT * FROM students;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StudentsModel student = new StudentsModel();

                        student.StudentId = (int)reader["StudentId"];
                        student.StudentName = reader["StudentName"].ToString();
                        student.StudentAddress = reader["StudentAddress"].ToString();
                        student.StudentAge = (int)reader["StudentAge"];

                        studentList.Add(student);
                    }
                }
            }
            return View(studentList);
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(StudentsModel student)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                const string query = "INSERT INTO Students (StudentName, StudentAddress, StudentAge) values (@StudentName, @StudentAddress, @StudentAge);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@StudentName", student.StudentName);
                    command.Parameters.AddWithValue("@StudentAddress", student.StudentAddress);
                    command.Parameters.AddWithValue("@StudentAge", student.StudentAge);

                    command.ExecuteNonQuery();

                }
            }
            return RedirectToAction("ViewStudents");
        }
    


        [HttpGet]
        public IActionResult UpdateStudent(int studentid)
        {
            StudentsModel studentsmodel = null;

            const string querystring = "select * from students where studentid = @studentid";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(querystring, connection))
                {
                    command.Parameters.AddWithValue("@studentid", studentid);

                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        studentsmodel = new StudentsModel();

                        studentsmodel.StudentName = reader["StudentName"].ToString();
                        studentsmodel.StudentAddress = reader["StudentAddress"].ToString();
                        studentsmodel.StudentAge = (int)reader["StudentAge"];

                    }
                }
            }
            return View(studentsmodel);
        }

        [HttpPost]
        public IActionResult UpdateStudent(int StudentId, StudentsModel students)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string queryString = "UPDATE students SET StudentId = @StudentId, StudentName = @StudentName, StudentAddress = @StudentAddress, StudentAge = @StudentAge WHERE StudentId = @StudentId;";

                using (MySqlCommand command = new MySqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", students.StudentId);
                    command.Parameters.AddWithValue("@StudentName", students.StudentName);
                    command.Parameters.AddWithValue("@StudentAddress", students.StudentAddress);
                    command.Parameters.AddWithValue("@StudentAge", students.StudentAge);

                    connection.Open();

                    command.ExecuteNonQuery();

                }
            }
            return RedirectToAction("ViewStudents");
        }

        [HttpGet]
        public IActionResult DeleteStudent(int studentid)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string queryString = "DELETE FROM students WHERE StudentId = @StudentId;";

                using (MySqlCommand command = new MySqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentid);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("ViewStudents");
        }


        //[HttpPost]
        //public IActionResult DeleteStudent(int StudentId, StudentsModel students) 
        //{
        //    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
        //    {
        //        string queryString = "DELETE FROM students WHERE StudentId = @StudentId;";

        //        using (MySqlCommand command = new MySqlCommand(queryString, connection)) 
        //        {
        //            command.Parameters.AddWithValue("@StudentId", students.StudentId);
        //            command.Parameters.AddWithValue("@StudentName", students.StudentName);
        //            command.Parameters.AddWithValue("@StudentAddress", students.StudentAddress);
        //            command.Parameters.AddWithValue("@StudentAge", students.StudentAge);

        //            connection.Open();

        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    return RedirectToAction("ViewStudents");
        //}



    }
}