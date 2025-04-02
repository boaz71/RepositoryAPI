using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryAPI.Interfaces;
using System;
using System.Threading.Tasks;

namespace RepositoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepositoryService _repositoryService;

        public RepositoryController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchRepositories([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Keyword is required.");
            }

            try
            {
                var repositories = await _repositoryService.SearchRepositoriesAsync(keyword);
                return Ok(repositories);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetSecureData")]
        [Authorize(Roles = "admin")]
        public IActionResult GetSecureData()
        {
            return Ok(new { Message = "This is a secure endpoint!" });
        }
    }
}
