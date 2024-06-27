using FiapApi.Data;
using FiapApi.Models.Entities;
using FiapApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiapApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class ColetaDeLixoController : ControllerBase
{
    private readonly AppDbContext appDbContext;

    public ColetaDeLixoController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    // GET: api/ColetaDeLixo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ColetaDeLixo>>> GetColetaDeLixo()
    {
        return await appDbContext.ColetaDeLixo.ToListAsync();
    }

    // GET: api/ColetaDeLixo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ColetaDeLixo>> GetColetaDeLixo(int id)
    {
        var coletaDeLixo = await appDbContext.ColetaDeLixo.FindAsync(id);

        if (coletaDeLixo == null)
        {
            return NotFound();
        }

        return coletaDeLixo;
    }

    // POST: api/ColetaDeLixo
    [HttpPost]
    public async Task<ActionResult<ColetaDeLixo>> PostColetaDeLixo(ColetaDeLixo coletaDeLixo)
    {
        appDbContext.ColetaDeLixo.Add(coletaDeLixo);
        await appDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetColetaDeLixo), new { id = coletaDeLixo.Id }, coletaDeLixo);
    }

    // DELETE: api/ColetaDeLixo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteColetaDeLixo(int id)
    {
        var coletaDeLixo = await appDbContext.ColetaDeLixo.FindAsync(id);
        if (coletaDeLixo == null)
        {
            return NotFound();
        }

        appDbContext.ColetaDeLixo.Remove(coletaDeLixo);
        await appDbContext.SaveChangesAsync();

        return NoContent();
    }
    
    
}