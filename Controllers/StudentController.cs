using dotrestapiwithmongo.Models;
using dotrestapiwithmongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotrestapiwithmongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<List<Student>> Get() =>
            await _studentService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student == null)
                return NotFound();

            return student;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            await _studentService.CreateAsync(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Student studentIn)
        {
            var existing = await _studentService.GetAsync(id);
            if (existing == null)
                return NotFound();

            studentIn.Id = id;
            await _studentService.UpdateAsync(id, studentIn);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student == null)
                return NotFound();

            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
