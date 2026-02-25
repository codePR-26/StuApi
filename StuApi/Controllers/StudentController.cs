using Microsoft.AspNetCore.Mvc;
using StuApi.Models;
using StuApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace StuApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound("Student not found");

            return Ok(student);
        }

        // POST
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student newStudent)
        {
            _context.Students.Add(newStudent);
            _context.SaveChanges();

            return Ok(newStudent);
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound("Student not found");

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Course = updatedStudent.Course;

            _context.SaveChanges();

            return Ok(student);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound("Student not found");

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Ok("Student deleted successfully");
        }
    }
}
