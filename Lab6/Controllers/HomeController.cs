
using Lab6;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly Lab6Context _context;

        public HomeController(Lab6Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ContractService> Get()
        {
            var data = _context.ContractServices
                .Include(e => e.Contract)
                .Include(e => e.Service)
                .ToList();
            return data;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ContractService insuranceCase = _context.ContractServices.FirstOrDefault(e => e.Id == id);
            if (insuranceCase == null)
                return NotFound();

            return new ObjectResult(insuranceCase);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContractService insuranceCase)
        {
            if (insuranceCase == null)
            {
                return BadRequest();
            }

            _context.ContractServices.Add(insuranceCase);
            _context.SaveChanges();
            return Ok(insuranceCase);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ContractService insuranceCase)
        {
            if (insuranceCase == null)
            {
                return BadRequest();
            }

            if (!_context.ContractServices.Any(e => e.Id == insuranceCase.Id))
            {
                return NotFound();
            }

            _context.Update(insuranceCase);
            _context.SaveChanges();
            return Ok(insuranceCase);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ContractService insuranceCase = _context.ContractServices.FirstOrDefault(e => e.Id == id);
            if (insuranceCase == null)
            {
                return NotFound();
            }

            _context.ContractServices.Remove(insuranceCase);
            _context.SaveChanges();
            return Ok(insuranceCase);
        }
    }
}