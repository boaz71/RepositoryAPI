namespace RepositoryAPI.Interfaces
{
    public interface IRepositoryService
    {
         Task<object> SearchRepositoriesAsync(string keyword);

    }
}
