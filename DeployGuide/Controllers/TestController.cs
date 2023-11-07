using System.Text.Json;
using DeployGuide.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DeployGuide.Controllers;

[ApiController]
[Route("test")]
public class TestController: ControllerBase
{
    private readonly DataContext _context;
    private readonly IDistributedCache _cache;

     public TestController(DataContext context, IDistributedCache cache)
     {
         _context = context;
         _cache = cache;
     }

     [HttpGet]
     public async Task<IActionResult> Test()
     {
         var user = _context.Users.FirstOrDefault();

         if (user == null) return BadRequest("User not found");

         await _cache.SetStringAsync(user.Id.ToString(), JsonSerializer.Serialize(user), new DistributedCacheEntryOptions
         {
             AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
         });

     return Ok("Hello world!" + $"{user?.Id} {user?.Name} {user?.Email}");
    }

    [HttpGet("redis/{id}")]
    public async Task<IActionResult> TestRedis(string id)
    {
        var user = await _cache.GetStringAsync(id);
        var userEntity = JsonSerializer.Deserialize<UserEntity>(user);
        
        return Ok($"From redis: {userEntity?.Id} {userEntity?.Email} {userEntity?.Name} userstring: {user}");
    }

    [HttpPost]
    public async Task<IActionResult> AddUser()
    {
        var user = new UserEntity
        {
            Name = "Random",
            Email = "random@mail.ru"
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok();
    }
}