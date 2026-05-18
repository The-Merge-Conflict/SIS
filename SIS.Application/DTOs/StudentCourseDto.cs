namespace SIS.Application.DTOs
{
    public class StudentCourseDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}