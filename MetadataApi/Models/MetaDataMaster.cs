using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetadataApi.Models;

[Table("MetaDataMaster")]
public class MetaDataMaster
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("abstract")]
    public string? Abstract { get; set; }

    [Column("language")]
    public string? Language { get; set; }

    [Column("topic_categories")]
    public string[]? TopicCategories { get; set; }

    [Column("keywords")]
    public string[]? Keywords { get; set; }

    [Required]
    [Column("temporal_start_date")]
    public DateOnly TemporalStartDate { get; set; }

    [Required]
    [Column("temporal_end_date")]
    public DateOnly TemporalEndDate { get; set; }

    [Required]
    [Column("metadata_date")]
    public DateOnly MetadataDate { get; set; }

    [Column("crs_code")]
    public string? CrsCode { get; set; }

    [Column("crs_name")]
    public string? CrsName { get; set; }

    [Column("projection_detail")]
    public string? ProjectionDetail { get; set; }

    [Column("geographic_extent")]
    public string? GeographicExtent { get; set; }

    [Column("distributor_name")]
    public string? DistributorName { get; set; }

    [Column("distribution_format")]
    public string? DistributionFormat { get; set; }

    [Column("transfer_url")]
    public string? TransferUrl { get; set; }

    [Required]
    [Column("contact_id")]
    public Guid ContactId { get; set; }

    [ForeignKey(nameof(ContactId))]
    public virtual Contact? Contact { get; set; }

    [Column("lineage")]
    public string? Lineage { get; set; }

    [Column("completeness_report")]
    public string? CompletenessReport { get; set; }

    [Column("positional_accuracy")]
    public string? PositionalAccuracy { get; set; }


    [Column("metadata_author")]
    public string? MetadataAuthor { get; set; }

    [Column("metadata_standard_name")]
    public string? MetadataStandardName { get; set; }

    [Column("metadata_standard_version")]
    public string? MetadataStandardVersion { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}