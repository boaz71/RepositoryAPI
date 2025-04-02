using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RepositoryAPI.Interfaces;

namespace RepositoryAPI.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly HttpClient _httpClient;

        public RepositoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> SearchRepositoriesAsync(string keyword)
        {
            var githubApiUrl = $"https://api.github.com/search/repositories?q={keyword}";
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubSearchApp");

            var response = await _httpClient.GetAsync(githubApiUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Error fetching data from GitHub API.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var repositories = JsonSerializer.Deserialize<object>(content);

            return repositories;
        }
    }
}
