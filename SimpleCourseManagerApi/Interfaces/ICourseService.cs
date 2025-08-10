using SimpleCourseManagerApi.Dtos.Request;
using SimpleCourseManagerApi.Dtos.Response;
using SimpleCourseManagerApi.Models;
using SimpleCourseManagerApi.Utils;

namespace SimpleCourseManagerApi.Interfaces
{
    public interface ICourseService
    {
        CourseResponseDto CreateCourse(CourseRequestDto course);
        bool DeleteCourse(int id);
        CourseResponseDto EditCourse(int id, CourseRequestDto course);
        IList<Course> GetAllCourses();
        Course? GetCourseById(int id);
    }
}
