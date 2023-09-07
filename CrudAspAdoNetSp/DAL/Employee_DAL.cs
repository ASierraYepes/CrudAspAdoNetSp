using CrudAspAdoNetSp.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CrudAspAdoNetSp.DAL
{
    public class Employee_DAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            return Configuration.GetConnectionString("CadenaSQL");
        }

        public List<Employee> GetAll()
        {
            List<Employee> employeeList = new List<Employee>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_Employees]";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    Employee employee = new Employee();

                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.FirstName = (string)dr["FirstName"];
                    employee.LastName = (string)dr["LastName"];
                    employee.DateOfBirth = (DateTime)dr["DateOfBirth"];
                    employee.Email = (string)dr["Email"];
                    employee.Salary = (double)dr["Salary"];

                    employeeList.Add(employee);
                }
                _connection.Close();
            }
            return employeeList;
        }

        public Employee GetById(int id)
        {
            Employee employee = new Employee();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_EmployeeById]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.FirstName = (string)dr["FirstName"];
                    employee.LastName = (string)dr["LastName"];
                    employee.DateOfBirth = (DateTime)dr["DateOfBirth"];
                    employee.Email = (string)dr["Email"];
                    employee.Salary = (double)dr["Salary"];
                }
                _connection.Close();
            }
            return employee;
        }

        public bool Insert(Employee model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Insert_Employee]";
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public bool Update(Employee model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Update_Employee]";
                _command.Parameters.AddWithValue("@Id", model.Id);
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            int DeletedRowCount = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Delete_Employee]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                DeletedRowCount = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return DeletedRowCount > 0 ? true : false;
        }
    }
}
