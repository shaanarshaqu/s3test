using s3test.Data.Models;
using s3test.Manager.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace s3test.Manager
{
    public class StudentManager:IStudentManager
    {
        private readonly SqlConnection dbConnection;
        public StudentManager(DbConnection dbConnection)
        {
            this.dbConnection = (SqlConnection) dbConnection;
        }
        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            try
            {
                List<Student> list = new List<Student>();
                //using (SqlCommand cmd = new SqlCommand("select * from Student", dbConnection))
                //{
                //    dbConnection.Open();
                //    SqlDataReader reader = cmd.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        list.Add(new Student() { Id = (string)reader["Id"], Name = (string)reader["Name"] });
                //    }
                //    return list;
                //}
                using (SqlCommand cmd = new SqlCommand("select * from Student", dbConnection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder1 = new SqlCommandBuilder(dataAdapter);
                    DataSet set = new DataSet();
                    dataAdapter.Fill(set, "students");
                    DataTable table = set.Tables["students"];
                    foreach (DataRow x in table.Rows)
                    {
                        list.Add(new Student
                        {
                            Id = (string)x["Id"],
                            Name = (string)x["Name"],
                        });
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> DeleteStudent(string id)
        {
            try
            {
                using(SqlCommand cmd = new SqlCommand("select * from Student", dbConnection))
                {
                    SqlDataAdapter adpter = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder1 = new SqlCommandBuilder(adpter);
                    DataSet set = new DataSet();
                    adpter.Fill(set, "students");
                    DataTable table = set.Tables["students"];
                    DataRow row = table.Rows.Find(id);
                    row.Delete();
                    return adpter.Update(table) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> AddStudent(Student student)
        {
            using (SqlCommand cmd = new SqlCommand("AddStudent", dbConnection))
            {
                dbConnection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
