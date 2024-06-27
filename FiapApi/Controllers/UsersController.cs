using FiapApi.Data;
using FiapApi.Models;
using FiapApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiapApi.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext appDbContext;

    public UsersController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    // GET - Users/Add
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    // POST - Users/Add
    [HttpPost]
    public async Task<IActionResult> Add(AddUserViewModel viewModel)
    {
        var user = new User
        {
            Name = viewModel.Name,
            Email = viewModel.Email,
            Key = viewModel.Key,
            Role = viewModel.Role,
            Password = viewModel.Password
        };

        await appDbContext.User.AddAsync(user);

        await appDbContext.SaveChangesAsync();
        
        return View();
    }
    
    // GET - Users/
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var users = await appDbContext.User.ToListAsync();
        
        // Returns a view with all users:
        return View(users);
        
        // Returns a JSON with all users:
        // return Json(users);

    }
    
    // GET - Users/{id}
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var foundUser = await appDbContext.User.FindAsync(id);

        return View(foundUser);
    }
    
    // POST - Users/
    [HttpPost]
    public async Task<IActionResult> Edit(User viewModel)
    {
        var foundUser = await appDbContext.User.FindAsync(viewModel.Id);

        if (foundUser is not null)
        {
            foundUser.Name = viewModel.Name;
            foundUser.Email = viewModel.Email;
            foundUser.Key = viewModel.Key;
            foundUser.Role = viewModel.Role;
            foundUser.Password = viewModel.Password;

            await appDbContext.SaveChangesAsync();
        }

        return RedirectToAction("List", "Users");
    }
    
    // POST - Users/
    public async Task<IActionResult> Delete(User viewModel)
    {
        var foundUser = await appDbContext.User.FindAsync(viewModel.Id);

        if (foundUser is not null)
        {
            appDbContext.User.Remove(foundUser);
            await appDbContext.SaveChangesAsync();
        }

        return RedirectToAction("List", "Users");
    }
    
    // GET Users/Coletas
    // Permite visualização das coletas sem chave da API pois tem acesso direto ao banco de dados MySQL
    // Rotas da API de coletas são protegidas conforme requisitos da atividade
    [HttpGet("Coletas")]
    public async Task<IActionResult> Coletas()
    {
        var coletas = await appDbContext.ColetaDeLixo.ToListAsync();
        return View(coletas);
    }
}