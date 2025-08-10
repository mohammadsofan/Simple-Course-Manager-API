using SimpleCourseManagerApi.Models;
using SimpleCourseManagerApi.Utils;

namespace SimpleCourseManagerApi.Dtos.Response
{
    public class CourseResponseDto
    {
        public bool Success { get; set; }
        public Course? Data { get; set; }
        public IList<Error>? Errors { get; set; }
    }
}
