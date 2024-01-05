using Microsoft.EntityFrameworkCore;
using MonitoringOfClasses.Domain.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringOfClasses.Infrastructure.Repository
{
    public class LessonRepository
    {
        private readonly Context _context;

        public LessonRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _context.Lessons
                .Include(p => p.Teacher)
                .OrderBy(p => p.Title).ToListAsync();
        }

        public async Task<Lesson> GetByIdAsync(int id)
        {
            return await _context.Lessons.Include(l => l.Teacher).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(Lesson lesson)
        {
            var existingTeacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == lesson.TeacherId);
            if (existingTeacher == null)
            {
                // Handle error: teacher not found
                throw new Exception("Teacher not found!");
            }

            lesson.Teacher = existingTeacher; // Set the existing teacher

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lesson lesson)
        {
            var existingLesson = await _context.Lessons
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(l => l.Id == lesson.Id);

            if (existingLesson == null)
            {
                // Handle error: lesson not found
                throw new Exception("Lesson not found!");
            }

            var existingTeacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == lesson.TeacherId);
            if (existingTeacher == null)
            {
                // Handle error: teacher not found
                throw new Exception("Teacher not found!");
            }

            existingLesson.Id = lesson.Id;
            existingLesson.Title = lesson.Title;
            existingLesson.Subject = lesson.Subject;
            existingLesson.Attendance = lesson.Attendance;
            existingLesson.TeacherId = existingTeacher.Id;
            existingLesson.Teacher = existingTeacher;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();
            }
        }
    }
}
