using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/lookups")]
public class LookupsController : ControllerBase
{
    [HttpGet("statuses")]
    public IActionResult GetStatuses()
    {
        var items = new[]
        {
            new { label = "Completed", value = "Completed" },
            new { label = "Historical archive", value = "Historical archive" },
            new { label = "Obsolete", value = "Obsolete" },
            new { label = "On going", value = "On going" },
            new { label = "Planned", value = "Planned" },
            new { label = "Required", value = "Required" },
            new { label = "Under development", value = "Under development" }
        };

        return Ok(items);
    }

    [HttpGet("presentation-forms")]
    public IActionResult GetPresentationForms()
    {
        var items = new[]
        {
            new { label = "Digital document", value = "Digital document" },
            new { label = "Digital image", value = "Digital image" },
            new { label = "Digital map", value = "Digital map" },
            new { label = "Digital model", value = "Digital model" },
            new { label = "Digital profile", value = "Digital profile" },
            new { label = "Digital table", value = "Digital table" },
            new { label = "Digital video", value = "Digital video" },
            new { label = "Hardcopy document", value = "Hardcopy document" },
            new { label = "Hardcopy image", value = "Hardcopy image" },
            new { label = "Hardcopy map", value = "Hardcopy map" }
        };

        return Ok(items);
    }

}