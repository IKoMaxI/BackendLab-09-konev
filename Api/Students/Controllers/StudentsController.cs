using Lab9.Api.Students.Contracts;
using Lab9.Api.Students.Models;
using Lab9.Api.Students.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab9.Api.Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(IOptions<StudentApiOptions> options) : ControllerBase
    {
        [HttpGet("/cfg")]
        public ActionResult GetConfig()
        {
            return Ok(options.Value.Code);
        }

        [HttpGet]
        public ActionResult<StudentResponseDTO[]> GetAllStudents()
        {
            var students = _students.Select(x => new StudentResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Group = x.Group,
                Specialization = x.Specialization
            }).ToArray();

            if (students.Any())
            {
                return Ok(students);
            }

            return NotFound("Список студентов пуст.");
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<StudentResponseDTO> GetStudent(int id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);

            if (student == null)
            {
                return NotFound("Студент не найден.");
            }

            return Ok(new StudentResponseDTO
            {
                Id = student.Id,
                Name = student.Name,
                Group = student.Group,
                Specialization = student.Specialization
            });
        }

        [HttpPost]
        public ActionResult CreateStudent([FromBody] StudentRequestDTO contract)
        {
            if (_students.Count >= options.Value.MaxCountStudents)
            {
                return BadRequest("Достигнуто максимальное количество студентов.");
            }

            if (!options.Value.AllowAdd)
            {
                return BadRequest("Данная опция недоступна.");
            }

            var id = _students.Count == 0 ? 1 : _students.Max(x => x.Id) + 1;

            var student = new Student
            {
                Id = id,
                Name = contract.Name,
                Group = contract.Group,
                Specialization = contract.Specialization
            };

            _students.Add(student);

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, new StudentResponseDTO
            {
                Id = student.Id,
                Name = student.Name,
                Group = student.Group,
                Specialization = student.Specialization
            });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStudent(int id, [FromBody] StudentRequestDTO contract)
        {
            if (!options.Value.AllowUpdate)
            {
                return BadRequest("Данная опция недоступна.");
            }

            var student = _students.FirstOrDefault(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = contract.Name;
            student.Group = contract.Group;
            student.Specialization = contract.Specialization;

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            if (!options.Value.AllowDelete)
            {
                return BadRequest("Данная опция недоступна.");
            }

            var index = _students.FindIndex(x => x.Id == id);

            if (index < 0)
            {
                return NotFound("Студент не найден.");
            }

            _students.RemoveAt(index);
            return NoContent();
        }

        private static readonly List<Student> _students = [];
    }
}
