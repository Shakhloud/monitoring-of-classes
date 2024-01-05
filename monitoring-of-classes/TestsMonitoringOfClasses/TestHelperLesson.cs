using System;
using Microsoft.EntityFrameworkCore;
using MonitoringOfClasses.Domain.Model;
using MonitoringOfClasses.Infrastructure;
using MonitoringOfClasses.Infrastructure.Repository;

namespace TestProjectTeacher
{
    public class TestHelperLesson
    {
        private readonly Context _context;

        public TestHelperLesson()
        {
            var contextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Tests")
                .Options;

            _context = new Context(contextOptions);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var teacher1 = new Teacher
            {
                Name = "John",
                Surname = "Doe",
                Phone = "123-456-789"
            };

            _context.Teachers.Add(teacher1);
            _context.SaveChanges();

            var lesson1 = new Lesson
            {
                Title = "Math Lesson",
                Subject = "Mathematics",
                Attendance = 20,
                TeacherId = teacher1.Id
            };

            var lesson2 = new Lesson
            {
                Title = "Science Class",
                Subject = "Science",
                Attendance = 15,
                TeacherId = teacher1.Id
            };

            _context.Lessons.AddRange(lesson1, lesson2);
            _context.SaveChanges();
        }

        public TeacherRepository TeacherRepository
        {
            get
            {
                return new TeacherRepository(_context);
            }
        }
        public LessonRepository LessonRepository
        {
            get
            {
                return new LessonRepository(_context);
            }
        }
    }
}
