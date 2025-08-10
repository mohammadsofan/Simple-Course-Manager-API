namespace SimpleCourseManagerApi.Interfaces
{
    public interface IValidator<TOutput,TInput>
    {
        TOutput IsValid(TInput value);
    }
}
