using MVCDBADO.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace MVCDBADO.DataAccess
{
    public class StudentDataAccess
    {
        string connstring = "data source=LAPTOP-BQF0DTHQ\\SQLEXPRESS;initial catalog=samp2024;integrated security=true;";

        public IEnumerable<Student> Display()
        {
            List<Student> listStudents = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spGetAll", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Student stud = new Student();
                    stud.Id = Convert.ToInt32(sdr["id"]);
                    stud.Name = sdr["name"].ToString()??string.Empty;
                    stud.Email = sdr["email"].ToString() ?? string.Empty;
                    stud.JoiningDate = DateTime.Parse(sdr["join_date"].ToString() ?? string.Empty);
                    listStudents.Add(stud);

                }
                return listStudents;
            }
        }
        public Student Insert(Student student)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand("spAddStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@stid", student.Id);
                cmd.Parameters.AddWithValue("@stname", student.Name);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@join_date", student.JoiningDate);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            return student;
        }
        public void Update(Student student)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand("spUpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@stid", student.Id);
                cmd.Parameters.AddWithValue("@stname", student.Name);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@join_date", student.JoiningDate);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            
        }
        public Student GetStudentById(int? id)
        {
            Student student = new Student();
            using (SqlConnection con = new SqlConnection(connstring))
            {
                string sqlQuery = "SELECT * FROM student WHERE id = "+id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    student.Id = Convert.ToInt32(rdr["id"]);
                    student.Name = rdr["name"].ToString() ?? string.Empty;
                    student.Email = rdr["email"].ToString() ?? string.Empty;
                    student.JoiningDate =DateTime.Parse(rdr["join_date"].ToString()??string.Empty);
                }
            }
            return student;
        }
       
    }
}
