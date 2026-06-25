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

        [HttpGet("search")]
        public async Task<IActionResult> SearchContacts(
    [FromQuery] string? term,
    CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Trim().Length < 2)
            {
                return Ok(Array.Empty<Contact>());
            }

            term = term.Trim();

            var contacts = await _context.Contacts
                .Where(c =>
                    EF.Functions.ILike(c.IndividualName!, $"%{term}%") ||
                    EF.Functions.ILike(c.Email!, $"%{term}%") ||
                    EF.Functions.ILike(c.OrganisationName!, $"%{term}%"))
                .OrderBy(c => c.IndividualName)
                .Take(20)
                .Select(c => new Contact
                {
                    Id = c.Id,
                    IndividualName = c.IndividualName,
                    Email = c.Email
                })
                .ToListAsync(cancellationToken);

            return Ok(contacts);
        }
    }
}