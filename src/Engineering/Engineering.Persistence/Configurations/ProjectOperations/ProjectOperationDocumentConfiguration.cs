using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Configurations.ProjectOperations;

public class ProjectOperationDocumentConfiguration : IEntityTypeConfiguration<ProjectOperationDocument>
{
    private const string _tableName = "ProjectOperationDocuments";
    public void Configure(EntityTypeBuilder<ProjectOperationDocument> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Url)
            .HasColumnType("nvarchar(1500)")
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.ProjectOperation)
               .WithMany(oo => oo.ProjectOperationDocuments)
               .HasForeignKey("ProjectOperationId")
               .HasPrincipalKey(nameof(ProjectOperation.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}
