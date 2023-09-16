using Casino.Data;
using Casino.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;

namespace Casino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaibhavController : ControllerBase
    {
        private readonly RegisterDbContext _registerDbContext;

        public VaibhavController(RegisterDbContext registerDbContext)
        {
            _registerDbContext = registerDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddRegister(Register register)
        {
            try
            {
                await _registerDbContext.Registers.AddAsync(register);
                await _registerDbContext.SaveChangesAsync();
                return Ok(register);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRegister()
        {
            try
            {
                //await _registerDbContext.Registers.ToListAsync();
                return Ok(await _registerDbContext.Registers.ToListAsync());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdRegister(int id)
        {
            try
            {
                var user = await _registerDbContext.Registers.FindAsync(id);
                return user == null ? NotFound() : Ok(user);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("Name")]
        public async Task<IActionResult> GetByIdRegister(int id, Register register)
        {
            try
            {
                if (id != register.Id) return BadRequest();
                _registerDbContext.Entry(register).State = EntityState.Modified;
                await _registerDbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var delete = await _registerDbContext.Registers.FindAsync(id);
                if (delete == null) return NotFound();
                _registerDbContext.Registers.Remove(delete);
                await _registerDbContext.SaveChangesAsync();
                return NoContent();
                
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
