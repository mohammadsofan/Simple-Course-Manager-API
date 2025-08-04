using SimpleCourseManagerApi.Dtos.Request;
using SimpleCourseManagerApi.Models;
using SimpleCourseManagerApi.Utils;

namespace SimpleCourseManagerApi.Interfaces
{
    public interface ICourseService
    {
        (bool success, Course? data, IList<Error>? errors) CreateCourse(CourseRequest course);
        bool DeleteCourse(int id);
        (bool success, IList<Error>? errors) EditCourse(int id, CourseRequest course);
        IList<Course> GetAllCourses();
        Course? GetCourseById(int id);
    }
}
