using FiapApi.Data;
using FiapApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FiapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColetaDeLixoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ColetaDeLixoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: api/ColetaDeLixo
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coletaDeLixoList = await _appDbContext.ColetaDeLixo.ToListAsync();
            return Ok(coletaDeLixoList);
        }

        // GET: api/ColetaDeLixo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coletaDeLixo = await _appDbContext.ColetaDeLixo.FindAsync(id);
            if (coletaDeLixo == null)
            {
                return NotFound();
            }
            return Ok(coletaDeLixo);
        }

        // POST: api/ColetaDeLixo
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ColetaDeLixo coletaDeLixo)
        {
            if (coletaDeLixo == null)
            {
                return BadRequest();
            }

            await _appDbContext.ColetaDeLixo.AddAsync(coletaDeLixo);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = coletaDeLixo.Id }, coletaDeLixo);
        }

        // DELETE: api/ColetaDeLixo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var coletaDeLixo = await _appDbContext.ColetaDeLixo.FindAsync(id);
            if (coletaDeLixo == null)
            {
                return NotFound();
            }

            _appDbContext.ColetaDeLixo.Remove(coletaDeLixo);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
