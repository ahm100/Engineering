using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.Projects;

public class SubProjectConfiguration : IEntityTypeConfiguration<SubProject>
{
    private const string _tableName = "SubProjects";

    public void Configure(EntityTypeBuilder<SubProject> builder)
    {
        builder.MetaConfiguration<SubProject, long>(_tableName);

        builder.Property(oo => oo.SubProjectCode)
            .HasColumnType("nvarchar(150)")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(oo => oo.SequenceNumber)
            .IsRequired();

        builder.Property(oo => oo.Name)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500);

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.HasIndex(oo => oo.SubProjectCode)
            .IsUnique();

        builder.HasIndex(oo => new { oo.ProjectId, oo.SequenceNumber })
            .IsUnique();

        builder.HasIndex(oo => oo.ProjectId);
        builder.HasIndex(oo => oo.Status);
        builder.HasIndex(oo => oo.Type);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.SubProjects)
            .HasForeignKey(oo => oo.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
