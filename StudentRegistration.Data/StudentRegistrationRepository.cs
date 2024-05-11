using StudentRegistration.Data.Interfaces;
using StudentRegistration.Domain.Models;
using System.Data;
using System.Data.SqlClient;

namespace StudentRegistration.Data
{
    public class StudentRegistrationRepository : IStudentRegistrationRepository
    {
        private readonly string _connectionString;
      
        public StudentRegistrationRepository(string connectionString) { 
            _connectionString = connectionString;
        }

        public Guid Save(Student student)
        {
            try
            {
                var id = Guid.NewGuid();
                var insertQuery = "insert into student values (@id, @firstname, @lastname, @email, @username, @password, @department)";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(insertQuery, connection);
                    // define parameters and their values
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 100).Value = id.ToString();
                    cmd.Parameters.Add("@firstname", SqlDbType.VarChar, 100).Value = student.FirstName;
                    cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 100).Value = student.LastName;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = student.Email;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar, 100).Value = student.Username;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar, 200).Value = student.Password;
                    cmd.Parameters.Add("@department", SqlDbType.VarChar, 50).Value = (int)student.Department;

                    // open connection, execute INSERT, close connection
                    connection.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return Guid.Empty;
        }

        public List<Student> GetAll()
        {
            var list = new List<Student>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var cmd = new SqlCommand("select * from student", connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var entity = new Student
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            FirstName = reader["firstname"].ToString(),
                            LastName = reader["lastname"].ToString(),
                            Email = reader["email"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                        };

                        list.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return list;
        }
    }
}
