using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Persistence.Configurations.EngineeringDocs;

public class ProjectDocConfiguration : IEntityTypeConfiguration<ProjectDoc>
{
    private const string _tableName = "ProjectDocs";

    public void Configure(EntityTypeBuilder<ProjectDoc> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Url)
            .HasMaxLength(256);

        builder.Property(oo => oo.Description)
            .HasMaxLength(1500);

        builder.Property(oo => oo.Code)
            .HasMaxLength(256);

        builder.HasOne(oo => oo.Project)
            .WithMany()
            .HasForeignKey(oo => oo.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Discipline)
            .WithMany(oo => oo.ProjectDocs)
            .HasForeignKey(oo => oo.DisciplineId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.DisciplineDoc)
            .WithMany(oo => oo.ProjectDocs)
            .HasForeignKey(oo => oo.DisciplineDocId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}