using Casino.Data;
using Casino.IRepository;
using Casino.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Casino.Controllers
{
    [EnableCors]
    [EnableRateLimiting("FixedWindow")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class VaibhavController : ControllerBase
    {
        private readonly IVaibhavRepository vaibhavRepository;
        private readonly RegisterDbContext _registerDbContext;

        public VaibhavController(IVaibhavRepository vaibhavRepository, RegisterDbContext registerDbContext)
        {
            this.vaibhavRepository = vaibhavRepository;
            _registerDbContext = registerDbContext;
        }



        // Follows Controller Pattern
        [HttpPost]
        public async Task<IActionResult> AddRegister(DTORegister dTORegister)
        {
            try
            {
                //Map DTO to Domain Model
                var dtoregister = new Register
                {
                    Name = dTORegister.Name,
                    Email = dTORegister.Email,
                    Pancard = dTORegister.Pancard,
                };

                
                //Map DTO to Domain Model for the Angular Application

                var response = new AngularDtoRegister
                {
                    Id = dtoregister.Id,
                    Name = dtoregister.Name,
                    Email = dtoregister.Email,
                    Pancard = dtoregister.Pancard,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Follows Repository Pattern
        [HttpGet]
        public async Task<IActionResult> GetRegister()
        {
            try
            {
                var data = vaibhavRepository.MyRegister();
                return Ok(data);
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

        [HttpGet("ExportToExcel")]
        public async Task <IActionResult> ExportToExcel()
        {
            try
            {
                var mydata = GetRegisterData();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.AddWorksheet(mydata, "My Sheet 1");
                    using (MemoryStream ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ViabhavExcel.xlsx");
                    }
                }
            } 
            catch (Exception ex) 
            {
                throw;
            }
        }



        [NonAction]
        private DataTable GetRegisterData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "VaibhvTable";
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("pancard", typeof(string));

            var list = _registerDbContext.Registers.ToList();
            if(list.Count > 0)
            {
                list.ForEach(Item =>
                {
                    dt.Rows.Add(Item.Id, Item.Name, Item.Email, Item.Pancard);
                });
            }
            return dt;
        }

    }
}
