namespace MonitoringOfClasses.API.DTO
{
    // DTO для урока (Lesson)
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int Attendance { get; set; }
        public int TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; } // DTO учителя, связанного с уроком
                                                // Другие необходимые свойства
    }
}
