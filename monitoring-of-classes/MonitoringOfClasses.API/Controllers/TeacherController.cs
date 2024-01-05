using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringOfClasses.API.DTO;
using MonitoringOfClasses.Domain.Model;
using MonitoringOfClasses.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonitoringOfClasses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherRepository _teacherRepository;

        public TeacherController(TeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDTO>>> GetAllTeachers()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            var teacherDTOs = new List<TeacherDTO>();

            foreach (var teacher in teachers)
            {
                teacherDTOs.Add(TeacherMapper.MapToDTO(teacher));
            }

            return Ok(teacherDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDTO>> GetTeacherById(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(TeacherMapper.MapToDTO(teacher));
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDTO>> AddTeacher(TeacherDTO teacherDTO)
        {
            var teacher = TeacherMapper.ToEntity(teacherDTO);
            await _teacherRepository.AddAsync(teacher);
            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, TeacherMapper.MapToDTO(teacher));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherDTO teacherDTO)
        {
            try
            {
                if (id != teacherDTO.Id)
                {
                    return BadRequest();
                }

                var existingTeacher = await _teacherRepository.GetByIdAsync(id);
                if (existingTeacher == null)
                {
                    return NotFound();
                }

                // Map DTO properties to the existing entity
                existingTeacher.Name = teacherDTO.Name;
                existingTeacher.Surname = teacherDTO.Surname;
                existingTeacher.Phone = teacherDTO.Phone;

                await _teacherRepository.UpdateAsync(existingTeacher);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the teacher: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var existingTeacher = await _teacherRepository.GetByIdAsync(id);
            if (existingTeacher == null)
            {
                return NotFound();
            }

            await _teacherRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
