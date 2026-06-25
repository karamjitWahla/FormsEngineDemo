using MetadataApi.Data;
using MetadataApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetadataApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExperimentsController : ControllerBase
    {
        private readonly MetadataDbContext _context;

        public ExperimentsController(MetadataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experiment>>> Get()
        {
            return await _context.Experiments
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Experiment>> Get(Guid id)
        {
            var item = await _context.Experiments.FindAsync(id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Experiment>> Post(Experiment model)
        {
            model.Id = Guid.NewGuid();
            model.CreatedAt = DateTime.UtcNow;

            _context.Experiments.Add(model);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Experiment model)
        {
            if (id != model.Id)
                return BadRequest();

            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Experiments.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.Experiments.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
