using Microsoft.AspNetCore.Mvc;
using MonitoringOfClasses.Domain.Model;
using MonitoringOfClasses.Infrastructure.Repository;
using MonitoringOfClasses.API.DTO; // Добавлено для использования DTO и маппера

namespace MonitoringOfClasses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly LessonRepository _lessonRepository;

        public LessonController(LessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository ?? throw new ArgumentNullException(nameof(lessonRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDTO>>> GetAllLessons()
        {
            var lessons = await _lessonRepository.GetAllAsync();
            var lessonDTOs = lessons.Select(LessonMapper.MapToDTO); // Преобразование в DTO
            return Ok(lessonDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDTO>> GetLessonById(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            var lessonDTO = LessonMapper.MapToDTO(lesson); // Преобразование в DTO
            return Ok(lessonDTO);
        }

        [HttpPost]
        public async Task<ActionResult<LessonDTO>> AddLesson(LessonDTO lessonDTO)
        {
            try
            {
                var lessonEntity = LessonMapper.ToEntity(lessonDTO); // Преобразование из DTO в сущность Lesson

                await _lessonRepository.AddAsync(lessonEntity);

                // После успешного добавления урока возвращаем DTO с созданным уроком
                var createdLessonDTO = LessonMapper.MapToDTO(lessonEntity);

                return CreatedAtAction(nameof(GetLessonById), new { id = createdLessonDTO.Id }, createdLessonDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, LessonDTO lessonDTO)
        {
            if (id != lessonDTO.Id)
            {
                return BadRequest();
            }

            var lesson = LessonMapper.ToEntity(lessonDTO); // Преобразование из DTO в сущность

            await _lessonRepository.UpdateAsync(lesson);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            await _lessonRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("mostVisited")]
        public async Task<ActionResult<LessonDTO>> GetMostVisitedLesson()
        {
            var mostVisitedLesson = (await _lessonRepository.GetAllAsync())
                .OrderByDescending(l => l.Attendance)
                .FirstOrDefault();

            if (mostVisitedLesson == null)
            {
                return NotFound();
            }

            return Ok(LessonMapper.MapToDTO(mostVisitedLesson)); // Преобразование в DTO
        }

        [HttpGet("leastVisited")]
        public async Task<ActionResult<LessonDTO>> GetLeastVisitedLesson()
        {
            var leastVisitedLesson = (await _lessonRepository.GetAllAsync())
                .OrderBy(l => l.Attendance)
                .FirstOrDefault();

            if (leastVisitedLesson == null)
            {
                return NotFound();
            }

            return Ok(LessonMapper.MapToDTO(leastVisitedLesson)); // Преобразование в DTO
        }
    }
}

