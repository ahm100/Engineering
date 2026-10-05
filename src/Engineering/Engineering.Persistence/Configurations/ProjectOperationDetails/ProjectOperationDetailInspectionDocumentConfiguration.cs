using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails;

public class ProjectOperationDetailInspectionDocumentConfiguration : IEntityTypeConfiguration<ProjectOperationDetailInspectionDocument>
{
    private const string _tableName = "ProjectOperationDetailInspectionDocuments";
    public void Configure(EntityTypeBuilder<ProjectOperationDetailInspectionDocument> builder)
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


        builder.HasOne(oo => oo.ProjectOperationDetailInspection)
               .WithMany(oo => oo.ProjectOperationDetailInspectionDocuments)
               .HasForeignKey("ProjectOperationDetailInspectionId")
               .HasPrincipalKey(nameof(ProjectOperationDetailInspection.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}
