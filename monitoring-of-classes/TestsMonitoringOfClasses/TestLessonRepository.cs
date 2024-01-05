using MonitoringOfClasses.Domain.Model;
using TestProjectTeacher;

namespace TestProjectLesson
{
    public class TestLessonRepository
    {
        [Fact]
        public async Task TestGetAllLessons()
        {
            var testHelper = new TestHelperLesson();
            var lessonRepository = testHelper.LessonRepository;

            var lessons = await lessonRepository.GetAllAsync();

            Assert.NotNull(lessons);
            Assert.NotEmpty(lessons);
        }

        [Fact]
        public async Task TestGetLessonById()
        {
            var testHelper = new TestHelperLesson();
            var lessonRepository = testHelper.LessonRepository;

            var lesson = await lessonRepository.GetByIdAsync(1);

            Assert.NotNull(lesson);
            Assert.Equal(1, lesson.Id);
        }

        [Fact]
        public async Task TestAddLesson()
        {
            var testHelper = new TestHelperLesson();
            var lessonRepository = testHelper.LessonRepository;
            var teacherRepository = testHelper.TeacherRepository;

            // Создание нового учителя
            var newTeacher = new Teacher
            {
                Name = "Alice",
                Surname = "Smith",
                Phone = "987-654-321"
            };

            // Добавление нового учителя в репозиторий учителей
            await teacherRepository.AddAsync(newTeacher);

            // Создание нового урока с использованием нового учителя
            var lesson = new Lesson
            {
                Title = "Test Lesson",
                Subject = "Test Subject",
                Attendance = 10,
                TeacherId = newTeacher.Id,
                Teacher = newTeacher
            };

            // Добавление урока с новым учителем
            await lessonRepository.AddAsync(lesson);

            // Получение добавленного урока
            var addedLesson = await lessonRepository.GetByIdAsync(lesson.Id);

            Assert.NotNull(addedLesson);
            Assert.Equal("Test Lesson", addedLesson.Title);
        }

        [Fact]
        public async Task TestUpdateLesson()
        {
            var testHelper = new TestHelperLesson();
            var lessonRepository = testHelper.LessonRepository;

            var lesson = await lessonRepository.GetByIdAsync(1); 

            lesson.Title = "Updated Title";

            await lessonRepository.UpdateAsync(lesson);

            var updatedLesson = await lessonRepository.GetByIdAsync(1); 

            Assert.NotNull(updatedLesson);
            Assert.Equal("Updated Title", updatedLesson.Title);
        }

        [Fact]
        public async Task TestDeleteLesson()
        {
            var testHelper = new TestHelperLesson();
            var lessonRepository = testHelper.LessonRepository;

            await lessonRepository.DeleteAsync(2);

            var deletedLesson = await lessonRepository.GetByIdAsync(2);

            Assert.Null(deletedLesson);
        }
    }
}
