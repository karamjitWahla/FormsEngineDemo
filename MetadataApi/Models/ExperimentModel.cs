using MetadataApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("tj_experiments")]
public class Experiment
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("contact_id")]
    public Guid? ContactId { get; set; }

    [ForeignKey(nameof(ContactId))]
    public Contact? Contact { get; set; }

    [Column("gender")]
    public string? Gender { get; set; }

    [Column("file_name")]
    public string? FileName { get; set; }

    [Column("agree")]
    public bool Agree { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}