using MonitoringOfClasses.Domain.Model;

namespace MonitoringOfClasses.API.DTO
{
    // Маппер для Teacher
    public class TeacherMapper
    {
        public static TeacherDTO MapToDTO(Teacher teacher)
        {
            return new TeacherDTO
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Surname = teacher.Surname,
                Phone = teacher.Phone,
            };
        }

        public static Teacher ToEntity(TeacherDTO teacherDTO)
        {
            return new Teacher
            {
                Id = teacherDTO.Id,
                Name = teacherDTO.Name,
                Surname = teacherDTO.Surname,
                Phone = teacherDTO.Phone,
            };
        }
    }
}
