using MetadataApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MetadataApi.Data;

public class MetadataDbContext : DbContext
{
    public MetadataDbContext(DbContextOptions<MetadataDbContext> options) : base(options)
    {
    }

    public DbSet<MetadataRecord> MetadataRecords => Set<MetadataRecord>();
    public DbSet<GraphicInfo> GraphicInfos => Set<GraphicInfo>();

    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MetadataRecord>(entity =>
        {
            entity.ToTable("identification_info");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.DateType).HasColumnName("date_type");
            entity.Property(e => e.DateValue).HasColumnName("date_value");
            entity.Property(e => e.TimeValue).HasColumnName("time_value");
            entity.Property(e => e.TimeZone).HasColumnName("time_zone");
            entity.Property(e => e.Edition).HasColumnName("edition");
            entity.Property(e => e.PresentationForm).HasColumnName("presentation_form");
            entity.Property(e => e.Abstract).HasColumnName("abstract");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.PointOfContact).HasColumnName("point_of_contact");
            entity.Property(e => e.MaintenanceUpdateFrequency).HasColumnName("maintenance_update_frequency");
            entity.Property(e => e.Contact).HasColumnName("contact");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<GraphicInfo>(entity =>
        {
            entity.ToTable("graphic_info");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.FileName).HasColumnName("file_name");
            entity.Property(e => e.FileDescription).HasColumnName("file_description");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(e => e.RecordId).IsUnique();
        });
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("tj_contacts");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.MetadataId)
                .HasColumnName("metadata_id");

            entity.Property(e => e.OrganisationName)
                .HasColumnName("organisation_name");

            entity.Property(e => e.IndividualName)
                .HasColumnName("individual_name");

            entity.Property(e => e.Email)
                .HasColumnName("email");

            entity.Property(e => e.Role)
                .HasColumnName("role");

            entity.Property(e => e.ContactType)
                .HasColumnName("contact_type");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.HasIndex(e => e.MetadataId);
        });
    }
}