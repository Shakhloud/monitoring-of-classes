namespace BlazorDomain
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int Attendance { get; set; }
        public int TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; } = new TeacherDTO();
    }
}
