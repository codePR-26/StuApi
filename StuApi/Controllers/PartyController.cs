using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuApi.Data;
using StuApi.Models;

namespace StuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PartyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/party
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var parties = await _context.Parties.ToListAsync();
            return Ok(parties);
        }

        // GET: api/party/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var party = await _context.Parties.FindAsync(id);

            if (party == null)
                return NotFound();

            return Ok(party);
        }

        // POST: api/party
        [HttpPost]
        public async Task<IActionResult> Create(Party party)
        {
            _context.Parties.Add(party);
            await _context.SaveChangesAsync();

            return Ok(party);
        }

        // PUT: api/party/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Party party)
        {
            if (id != party.Id)
                return BadRequest();

            _context.Entry(party).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(party);
        }

        // DELETE: api/party/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var party = await _context.Parties.FindAsync(id);

            if (party == null)
                return NotFound();

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

        // POST: api/party/allowsignup
        [HttpPost("allowsignup")]
        public async Task<IActionResult> AllowSignUp(int partyId)
        {
            var party = await _context.Parties
                .FirstOrDefaultAsync(p => p.Id == partyId);

            if (party == null)
                return Ok(new { isExternal = true });

            return Ok(new { isExternal = party.IsExternal });
        }
    }
}