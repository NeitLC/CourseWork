namespace CourseWork.Domain.Interfaces
{
    public interface IEntityWithId<T>
    {
        T Id { get; set; }
    }
}