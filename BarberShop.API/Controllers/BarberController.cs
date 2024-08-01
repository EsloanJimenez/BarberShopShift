using BarberShop.Domain.Entities;
using BarberShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BarberShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {
        private readonly IBarberRepository _barberRepository;
        public BarberController(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }
        // GET: api/<BarberController>
        [HttpGet("GetBarber")]
        public async Task<IActionResult> Get()
        {
            var barber = _barberRepository.GetBarber();
            return Ok(barber);
        }

        // GET api/<BarberController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var barber = await _barberRepository.Get(id);
            if (barber is null)
                return NotFound();

            return Ok(barber);
        }

        // POST api/<BarberController>
        [HttpPost("CreateBarber")]
        public async Task<IActionResult> Post([FromBody] Barber barberAddModel)
        {
            try
            {
                if (barberAddModel is null)
                    return BadRequest("Barber data is null");

                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                barberAddModel.CreationDate = DateTime.Now;
                barberAddModel.UserCreation = 1;

                await _barberRepository.Save(barberAddModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }

            return Ok(barberAddModel);
        }

        // PUT api/<BarberController>/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Barber barber)
        {
            barber.UserMod = 1;

            try
            {
                await _barberRepository.Update(barber);
            }catch(DbUpdateConcurrencyException)
            {
                if (!await _barberRepository.Exists(cd => cd.BarberId == id))
                    return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE api/<BarberController>/5
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(int barberId)
        {
            var barber = await _barberRepository.Get(barberId);

            if (barber is null)
                return NotFound();

            await _barberRepository.Remove(barber);

            return NoContent();
        }
    }
}
