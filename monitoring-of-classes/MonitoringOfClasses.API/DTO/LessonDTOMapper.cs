using MonitoringOfClasses.Domain.Model;

namespace MonitoringOfClasses.API.DTO
{
    public class LessonMapper
    {
        public static LessonDTO MapToDTO(Lesson lesson)
        {
            return new LessonDTO
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Subject = lesson.Subject,
                Attendance = lesson.Attendance,
                TeacherId = lesson.TeacherId,
                Teacher = TeacherMapper.MapToDTO(lesson.Teacher), // Преобразование учителя в DTO
                                                                  // Другие свойства, если необходимо
            };
        }

        public static Lesson ToEntity(LessonDTO lessonDTO)
        {
            return new Lesson
            {
                Id = lessonDTO.Id,
                Title = lessonDTO.Title,
                Subject = lessonDTO.Subject,
                Attendance = lessonDTO.Attendance,
                TeacherId = lessonDTO.TeacherId,
                // Преобразование DTO учителя обратно в сущность Teacher для свойства Teacher
                Teacher = TeacherMapper.ToEntity(lessonDTO.Teacher),
                // Другие свойства, если необходимо
            };
        }
    }
}
