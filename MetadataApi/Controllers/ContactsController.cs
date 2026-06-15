using MetadataApi.Data;
using MetadataApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetadataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly MetadataDbContext _context;

        public ContactsController(MetadataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetAll()
        {
            var records = await _context.Contacts
                .OrderBy(x => x.Id)
                .ToListAsync();

            return Ok(records);
        }
    }
}