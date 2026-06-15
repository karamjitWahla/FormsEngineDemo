using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetadataApi.Models;

[Table("identification_info")]
public class MetadataRecord
{
    public long Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? DateType { get; set; }

    public DateOnly? DateValue { get; set; }

    public TimeOnly? TimeValue { get; set; }

    public string? TimeZone { get; set; }

    public string? Edition { get; set; }

    public string? PresentationForm { get; set; }

    [Required]
    public string Abstract { get; set; } = string.Empty;

    public string? Purpose { get; set; }

    public string? Status { get; set; }

    public string? PointOfContact { get; set; }

    [Required]
    public string MaintenanceUpdateFrequency { get; set; } = string.Empty;

    public string? Contact { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

public class GraphicInfo
{
    public int Id { get; set; }
    public int RecordId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string? FileDescription { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class Contact
{
    public Guid Id { get; set; }
    public long MetadataId { get; set; }
    public string? OrganisationName { get; set; }
    public string? IndividualName { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? ContactType { get; set; }
    public DateTime CreatedAt { get; set; }
}