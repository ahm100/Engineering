using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Persistence.Configurations.EngineeringDocs;

public class ProjectDocHistoryConfiguration
    : IEntityTypeConfiguration<ProjectDocHistory>
{
    public void Configure(EntityTypeBuilder<ProjectDocHistory> builder)
    {
        builder.ToTable("ProjectDocHistories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Url)
       .HasMaxLength(256);

        builder.Property(x => x.Code)
            .HasMaxLength(256);

        builder.Property(x => x.Description)
            .HasMaxLength(1500);

        builder.HasOne(x => x.ProjectDoc)
            .WithMany()
            .HasForeignKey(x => x.ProjectDocId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}