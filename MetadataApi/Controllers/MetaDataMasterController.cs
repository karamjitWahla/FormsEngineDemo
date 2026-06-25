using MetadataApi.Data;
using MetadataApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetadataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetaDataMasterController : ControllerBase
{
    private readonly MetadataDbContext _context;

    public MetaDataMasterController(MetadataDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _context.MetaDataMasters
            .Include(x => x.Contact)
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await _context.MetaDataMasters
            .Include(x => x.Contact)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post(MetaDataMaster model)
    {
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = DateTime.UtcNow;

        _context.MetaDataMasters.Add(model);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Get),
            new { id = model.Id },
            model);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] MetaDataMaster model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id != model.Id)
            return BadRequest();

        var existing = await _context.MetaDataMasters
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existing == null)
            return NotFound();

        _context.Entry(existing).CurrentValues.SetValues(model);

        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _context.MetaDataMasters
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return NotFound();

        _context.MetaDataMasters.Remove(item);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}