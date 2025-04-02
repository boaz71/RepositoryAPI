namespace RepositoryAPI.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username);
    }
}
