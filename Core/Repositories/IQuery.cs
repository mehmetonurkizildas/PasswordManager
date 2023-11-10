namespace Core.Repositories
{
    public interface IQuery<T>
    {
        Task<IQueryable<T>> Query();
    }
}
