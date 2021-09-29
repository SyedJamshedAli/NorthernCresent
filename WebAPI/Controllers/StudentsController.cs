using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IRepository<Student> _studentRepository;
    

        public StudentsController(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            
        }
        [HttpGet]
        public IEnumerable<Student> GetStudent()
        {
            return _studentRepository.GetAll();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public Student GetStudent(int id)
        {
            var student = _studentRepository.GetById(id);
            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public Student PutStudent(Student student)
        {
            var existingStudent = _studentRepository.GetById(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                _studentRepository.Update(existingStudent);
            }
            return student;
        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public Student PostStudent(Student student)
        {
            _studentRepository.Insert(student);
            return student;
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student != null)
            {
                _studentRepository.Delete(student.Id);
            }

            return student;
        }

    }
}
