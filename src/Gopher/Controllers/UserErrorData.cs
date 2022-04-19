using Gopher.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gopher.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserErrorDataController : ControllerBase
{
    private static readonly string[] ErrorMessages = new[]
    {
        "Failed Login Attempt", "Failed to Save", "TODO", "TODO", "TODO", "TODO", "TODO", "TODO", "TODO", "TODO"
    };

    private readonly ILogger<UserErrorDataController> _logger;

    public UserErrorDataController(ILogger<UserErrorDataController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<UserErrorData> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new UserErrorData
        {
            Date = DateTime.Now.AddDays(index),
            UserID = Random.Shared.Next(-1, 1000),
            ErrorID = Random.Shared.Next(1, 200),
            Message = ErrorMessages[Random.Shared.Next(ErrorMessages.Length)]
        })
        .ToArray();
    }
}

