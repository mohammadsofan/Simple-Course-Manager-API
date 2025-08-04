using SimpleCourseManagerApi.Dtos.Request;
using SimpleCourseManagerApi.Interfaces;
using SimpleCourseManagerApi.Utils;

namespace SimpleCourseManagerApi.Validators
{
    public class CourseValidatorResult
    {
        public bool IsValid {  get; set; }
        public IList<Error> Errors { get; set; } = new List<Error>();
    }

    public class CourseValidator:IValidator<CourseValidatorResult,CourseRequest> { 
        public CourseValidatorResult IsValid(CourseRequest course)
        {
            IList<Error> errors = new List<Error>();
            if (course.Name is null || course.Name.Length < 3)
            {
                errors.Add(new Error
                {
                    Field = "Name",
                    Message = "Name must be 3 characters at least"
                });
            }
            if(course.Description is null || course.Description.Length < 3)
            {
                errors.Add(new Error
                {
                    Field = "Description",
                    Message = "Description must be 3 characters at least"
                });
            }
            if(course.Price < 0)
            {
                errors.Add(new Error
                {
                    Field = "Price",
                    Message = "Price must be positive value"
                });
            }
            if(course.Subject is null || course.Subject.Length < 2)
            {
                errors.Add(new Error
                {
                    Field = "Subject",
                    Message = "Subject must be 2 characters at least"
                });
            }

            return new CourseValidatorResult()
            {
                IsValid = errors.Count == 0,
                Errors = errors
            };
        }
    }
}
 