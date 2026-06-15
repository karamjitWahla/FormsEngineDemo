using MetadataApi.Data;
using MetadataApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetadataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly MetadataDbContext _context;

        public MetaDataController(MetadataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MetadataRecord>>> GetAll()
        {
            var records = await _context.MetadataRecords
     .OrderBy(x => x.Id)
     .ToListAsync();


            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MetadataRecord>> Get(long id)
        {
            var record = await _context.MetadataRecords.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<MetadataRecord>> Post([FromBody] MetadataRecord record)
        {
            record.CreatedAt = DateTime.UtcNow;
            record.UpdatedAt = DateTime.UtcNow;

            _context.MetadataRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMetadata(long id, [FromBody] MetadataRecord updated)
        {
            var record = await _context.MetadataRecords.FindAsync(id);

            if (record == null)
                return NotFound();

            record.Title = updated.Title;
            record.DateType = updated.DateType;
            record.DateValue = updated.DateValue;
            record.TimeValue = updated.TimeValue;
            record.TimeZone = updated.TimeZone;
            record.Edition = updated.Edition;
            record.PresentationForm = updated.PresentationForm;
            record.Abstract = updated.Abstract;
            record.Purpose = updated.Purpose;
            record.Status = updated.Status;
            record.PointOfContact = updated.PointOfContact;
            record.MaintenanceUpdateFrequency = updated.MaintenanceUpdateFrequency;
            record.Contact = updated.Contact;
            record.UpdatedAt = DateTime.UtcNow;
            record.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var record = await _context.MetadataRecords.FindAsync(id);

            if (record == null)
                return NotFound();

            _context.MetadataRecords.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}