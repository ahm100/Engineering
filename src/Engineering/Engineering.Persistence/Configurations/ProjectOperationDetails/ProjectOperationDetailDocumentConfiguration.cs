using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails;

public class ProjectOperationDetailDocumentConfiguration : IEntityTypeConfiguration<ProjectOperationDetailDocument>
{
    private const string _tableName = "ProjectOperationDetailDocuments";
    public void Configure(EntityTypeBuilder<ProjectOperationDetailDocument> builder)
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


        builder.HasOne(oo => oo.ProjectOperationDetail)
               .WithMany(oo => oo.ProjectOperationDetailDocuments)
               .HasForeignKey("ProjectOperationDetailId")
               .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}
