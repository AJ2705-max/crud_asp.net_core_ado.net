using MySql.Data.MySqlClient;
using TestDemo1.Controllers;
using TestDemo1.Models;

public class TeacherService : ITeacherService
{
    private readonly string _connectionString;

    public string StudentName { get; set; }
    public string teacherName { get; set; }
    public string Standard { get; set; }
    public string Section { get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    

    public TeacherService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public void AddStandard(StandardController standard)
    {
        throw new NotImplementedException();
    }

    

    public void AddStandard(ITeacherService teacherService)
    {
        string teacherName = teacherService.GetTeacherNames();

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            const string query = "INSERT INTO StudentStandard (StudentName, teacherName, Standard, Section, Age, Gender, Address) VALUES (@StudentName, @teacherName, @Standard, @Section, @Age, @Gender, @Address);";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@StudentName", teacherService.StudentName);
                command.Parameters.AddWithValue("@teacherName", teacherName);
                command.Parameters.AddWithValue("@Standard", teacherService.Standard);
                command.Parameters.AddWithValue("@Section", teacherService.Section);
                command.Parameters.AddWithValue("@Age", teacherService.Age);
                command.Parameters.AddWithValue("@Gender", teacherService.Gender);
                command.Parameters.AddWithValue("@Address", teacherService.Address);

                command.ExecuteNonQuery();
            }
        }
    }

    public dynamic GetTeacherNames()
    {
        return GetTeacherNames()?.FirstOrDefault();
    }

    public void AddStandard(StandardModel standard)
    {
        throw new NotImplementedException();
    }
}