using MetadataApi.Data;
using MetadataApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetadataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphicInfoController : ControllerBase
    {
        private readonly MetadataDbContext _context;

        public GraphicInfoController(MetadataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GraphicInfo>>> GetAll()
        {
            var records = await _context.GraphicInfos
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return Ok(records);
        }


        [HttpGet("{recordId}")]
        public async Task<ActionResult<GraphicInfo>> Get(int recordId)
        {
            var record = await _context.GraphicInfos
                .FirstOrDefaultAsync(x => x.RecordId == recordId);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<GraphicInfo>> Post([FromBody] GraphicInfo record)
        {
            record.CreatedAt = DateTime.UtcNow;
            record.UpdatedAt = DateTime.UtcNow;

            _context.GraphicInfos.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { recordId = record.RecordId }, record);
        }

        [HttpPut("{recordId}")]
        public async Task<IActionResult> UpsertGraphicInfo(int recordId, [FromBody] GraphicInfo updated)
        {
            var record = await _context.GraphicInfos
                .FirstOrDefaultAsync(x => x.RecordId == recordId);

            if (record == null)
            {
                record = new GraphicInfo
                {
                    RecordId = recordId,
                    FileName = updated.FileName,
                    FileDescription = updated.FileDescription,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.GraphicInfos.Add(record);
            }
            else
            {
                record.FileName = updated.FileName;
                record.FileDescription = updated.FileDescription;
                record.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return Ok(record);
        }

        [HttpDelete("{recordId}")]
        public async Task<IActionResult> Delete(int recordId)
        {
            var record = await _context.GraphicInfos
                .FirstOrDefaultAsync(x => x.RecordId == recordId);

            if (record == null)
                return NotFound();

            _context.GraphicInfos.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
