using SIS.Domain.Common;

namespace SIS.Domain
{
    public class Student : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public string? StudentNumber { get; set; }
        public Guid IdentityUserId { get; set; }

        // Navigation Property
        public ICollection<StudentCourse>? StudentCourses { get; set; }
    }
}
