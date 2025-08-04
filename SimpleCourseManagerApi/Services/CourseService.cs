using SimpleCourseManagerApi.Dtos.Request;
using SimpleCourseManagerApi.Interfaces;
using SimpleCourseManagerApi.Models;
using SimpleCourseManagerApi.Utils;
using SimpleCourseManagerApi.Validators;

namespace SimpleCourseManagerApi.Services
{
    public class CourseService : ICourseService
    {
        public static IList<Course> _courses= new List<Course>();
        private readonly IValidator<CourseValidatorResult, CourseRequest> validator;

        public CourseService(IValidator<CourseValidatorResult, CourseRequest> validator)
        {
            this.validator = validator;
        }
        public (bool success,Course? data,IList<Error>? errors) CreateCourse(CourseRequest courseDto)
        {
            var result = validator.IsValid(courseDto);
            if (!result.IsValid)
            {
                return (result.IsValid, null,result.Errors);
            }
            int maxId;
            if (_courses.Count == 0) maxId = 0;
            else maxId = _courses.Max(c => c.Id);
            var course = new Course() { 
                Id= maxId + 1,
                Name = courseDto.Name,
                Description = courseDto.Description,
                Subject = courseDto.Subject,
                Price = courseDto.Price,
            };
            _courses.Add(course);
            return (result.IsValid,course,null);
        }

        public bool DeleteCourse(int id)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            if(course is null) {
                return false;
            }
            _courses.Remove(course);
            return true;
        }

        public (bool success, IList<Error>? errors) EditCourse(int id, CourseRequest courseDto)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            if (course is null)
            {
                return (false,null);
            }
            var result = validator.IsValid(courseDto);
            if (!result.IsValid)
            {
                return (result.IsValid, result.Errors);
            }
            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.Subject = courseDto.Subject;
            course.Price = courseDto.Price;

            return (result.IsValid,null);
        }

        public IList<Course> GetAllCourses()
        {
            return _courses;
        }

        public Course? GetCourseById(int id)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }
    }
}
