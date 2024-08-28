using Microsoft.AspNetCore.Mvc;
using s3test.Data.Models;
using s3test.Manager.Interfaces;

namespace s3test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentManager studentManager;
        public StudentsController(IStudentManager studentManager) 
        { 
            this.studentManager = studentManager;
        }


        [HttpGet]
        public async Task<IActionResult> ListAllStudents()
        {
            try
            {
                return Ok(await studentManager.GetAllStudent());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudent(string Id)
        {
            try
            {
                return Ok(await studentManager.DeleteStudent(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewStudent(Student stdt)
        {
            try
            {
                return Ok(await studentManager.AddStudent(stdt));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
