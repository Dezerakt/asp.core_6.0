using asp_project.Models;
using asp_project.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly ApplicationContext _context;
        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var responce = await _context.Users.ToListAsync();
            return Ok(responce);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var responce = await _context.Users.FindAsync(id);
            return Ok(responce);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (model.Email != null)
            {
                return BadRequest();
            }

            if (!_context.Users.Select(x => x.Email).ToList().Contains(model.Email))
            {
                if (model.Password != null)
                {
                    model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                }
                _context.Users.Add(model);
                await _context.SaveChangesAsync();
            }
            else return BadRequest(new { errorText = "This Email is already used" });
            return Ok();

        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User model)
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
            var responce = await _context.Users.FindAsync(id);
            if (responce == null)
            {
                return NotFound();
            }
            _context.Users.Remove(responce);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
