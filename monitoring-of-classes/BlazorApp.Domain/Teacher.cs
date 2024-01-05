namespace BlazorDomain
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public ICollection<LessonDTO> Lessons { get; set; } = new List<LessonDTO>();
    }
}
