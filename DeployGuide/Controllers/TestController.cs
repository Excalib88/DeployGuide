using DeployGuide.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeployGuide.Controllers;

[ApiController]
[Route("test")]
public class TestController: ControllerBase
{
    //private readonly DataContext _context;

    // public TestController(DataContext context)
    // {
    //     _context = context;
    // }

    [HttpGet]
    public IActionResult Test()
    {
        //var user = _context.Users.FirstOrDefault();
        return Ok("Hello world!");
    }

    [HttpPost]
    public async Task<IActionResult> AddUser()
    {
        var user = new UserEntity
        {
            Name = "Random",
            Email = "random@mail.ru"
        };
        // await _context.Users.AddAsync(user);
        // await _context.SaveChangesAsync();

        return Ok();
    }
}