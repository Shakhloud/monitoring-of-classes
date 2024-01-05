using Xunit;
using MonitoringOfClasses.Domain.Model;
using MonitoringOfClasses.Infrastructure.Repository;
using System.Threading.Tasks;

namespace TestProjectTeacher
{
    public class TestTeacherRepository
    {
        [Fact]
        public async Task TestAddTeacher()
        {
            // Arrange
            var testHelper = new TestHelperTeacher();
            var teacherRepository = testHelper.TeacherRepository;

            var teacher = new Teacher
            {
                Name = "Alice",
                Surname = "Smith",
                Phone = "987-654-321"
            };

            // Act
            await teacherRepository.AddAsync(teacher);

            // Assert
            var result = await teacherRepository.GetByIdAsync(teacher.Id);
            Assert.Equal("Alice", result.Name);
        }

        [Fact]
        public async Task TestUpdateTeacher()
        {
            // Arrange
            var testHelper = new TestHelperTeacher();
            var teacherRepository = testHelper.TeacherRepository;

            var teacher = new Teacher
            {
                Name = "Bob",
                Surname = "Johnson",
                Phone = "123-456-789"
            };
            await teacherRepository.AddAsync(teacher);

            // Act
            teacher.Name = "David";
            await teacherRepository.UpdateAsync(teacher);

            // Assert
            var result = await teacherRepository.GetByIdAsync(teacher.Id);
            Assert.Equal("David", result.Name);
        }

        [Fact]
        public async Task TestDeleteTeacher()
        {
            // Arrange
            var testHelper = new TestHelperTeacher();
            var teacherRepository = testHelper.TeacherRepository;

            var teacher = new Teacher
            {
                Name = "Alice",
                Surname = "Smith",
                Phone = "987-654-321"
            };
            await teacherRepository.AddAsync(teacher);

            // Act
            await teacherRepository.DeleteAsync(teacher.Id);

            // Assert
            var result = await teacherRepository.GetByIdAsync(teacher.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task TestGetAllTeachers()
        {
            // Arrange
            var testHelper = new TestHelperTeacher();
            var teacherRepository = testHelper.TeacherRepository;

            await teacherRepository.AddAsync(new Teacher { Name = "Alice", Surname = "Smith", Phone = "987-654-321" });
            await teacherRepository.AddAsync(new Teacher { Name = "Bob", Surname = "Johnson", Phone = "123-456-789" });

            // Act
            var result = await teacherRepository.GetAllAsync();

            // Assert
            Assert.Equal(3, result.Count); // Since there's already one teacher added in TestHelperTeacher, the total count will be 3
        }
    }
}
