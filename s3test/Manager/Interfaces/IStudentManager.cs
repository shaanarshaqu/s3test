using s3test.Data.Models;

namespace s3test.Manager.Interfaces
{
    public interface IStudentManager
    {
        Task<IEnumerable<Student>> GetAllStudent();
        Task<bool> DeleteStudent(string id);
        Task<bool> AddStudent(Student student);
    }
}
