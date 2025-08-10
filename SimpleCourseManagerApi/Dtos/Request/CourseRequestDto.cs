namespace SimpleCourseManagerApi.Dtos.Request
{
    public class CourseRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
