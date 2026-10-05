using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectTypeConfiguration : IEntityTypeConfiguration<ProjectType>
{
    private const string TableName = "EngineeringProjectTypes";
    public void Configure(EntityTypeBuilder<ProjectType> builder)
    {
        builder.MetaActiveConfiguration<ProjectType, long>(TableName);

        builder.Property(oo => oo.ProjectTypeCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(oo => oo.ProjectTypeTitle)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);
    }
}