using SimpleCourseManagerApi.Dtos.Request;
using SimpleCourseManagerApi.Dtos.Response;
using SimpleCourseManagerApi.Interfaces;
using SimpleCourseManagerApi.Models;
using SimpleCourseManagerApi.Utils;
using SimpleCourseManagerApi.Validators;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace SimpleCourseManagerApi.Services
{
    public class CourseService : ICourseService
    {
        public static IList<Course> _courses= new List<Course>();
        private readonly IValidator<CourseValidatorResult, CourseRequestDto> validator;

        public CourseService(IValidator<CourseValidatorResult, CourseRequestDto> validator)
        {
            this.validator = validator;
        }
        public CourseResponseDto CreateCourse(CourseRequestDto courseDto)
        {
            var result = validator.IsValid(courseDto);
            if (!result.IsValid)
            {
                return new CourseResponseDto()
                {
                    Success = false,
                    Errors = result.Errors.ToList()
                };
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
            return new CourseResponseDto()
            {
                Success = true,
                Data = course
            };
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

        public CourseResponseDto EditCourse(int id, CourseRequestDto courseDto)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            if (course is null)
            {
                return new CourseResponseDto()
                {
                    Success = false,
                    Errors = new List<Error>() { new Error() { Field = "course", Message = "Course not found" } }
                };
            }
            var result = validator.IsValid(courseDto);
            if (!result.IsValid)
            {
                return new CourseResponseDto()
                {
                    Success = false,
                    Errors =result.Errors.ToList()
                };
            }
            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.Subject = courseDto.Subject;
            course.Price = courseDto.Price;

            return new CourseResponseDto()
            {
                Success = true
            };
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
