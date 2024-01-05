namespace MonitoringOfClasses.Domain.Model
{
    public class Lesson
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Subject { get; set; }

        public int Attendance { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; } // Ссылка на преподавателя

    }
}
