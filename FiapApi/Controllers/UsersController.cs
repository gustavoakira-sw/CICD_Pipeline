using FiapApi.Data;
using FiapApi.Models;
using FiapApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FiapApi.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext appDbContext;

    public UsersController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    // GET
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Add(AddUserViewModel viewModel)
    {
        var user = new User
        {
            Id = 0,
            Name = null,
            Email = null,
            Key = null,
            Role = null,
            Password = null
        };

        await appDbContext.User.AddAsync(user);

        await appDbContext.SaveChangesAsync();
        
        return View();
    }
}