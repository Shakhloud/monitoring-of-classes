using System;
using Microsoft.EntityFrameworkCore;
using MonitoringOfClasses.Domain.Model;
using MonitoringOfClasses.Infrastructure;
using MonitoringOfClasses.Infrastructure.Repository;

namespace TestProjectTeacher
{
    public class TestHelperTeacher
    {
        private readonly Context _context;

        public TestHelperTeacher()
        {
            var contextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestTeacher")
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
        }

        public TeacherRepository TeacherRepository
        {
            get
            {
                return new TeacherRepository(_context);
            }
        }
    }
}
