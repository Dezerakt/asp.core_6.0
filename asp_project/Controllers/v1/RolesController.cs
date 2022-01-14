using asp_project.Models;
using asp_project.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public readonly ApplicationContext _context;
        public RolesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var responce = await _context.Roles.ToListAsync();
            return Ok(responce);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var responce = await _context.Roles.FindAsync(id);
            return Ok(responce);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Role model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            _context.Roles.Add(model);

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Role model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            _context.Update(model);

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responce = await _context.Roles.FindAsync(id);
            if (responce == null)
            {
                return NotFound();
            }
            _context.Roles.Remove(responce);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

