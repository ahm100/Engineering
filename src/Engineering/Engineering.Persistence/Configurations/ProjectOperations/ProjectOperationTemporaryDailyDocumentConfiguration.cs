using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Configurations.ProjectOperations;

public class ProjectOperationTemporaryDailyDocumentConfiguration : IEntityTypeConfiguration<ProjectOperationTemporaryDailyDocument>
{
    private const string _tableName = "ProjectOperationTemporaryDailyDocuments";
    public void Configure(EntityTypeBuilder<ProjectOperationTemporaryDailyDocument> builder)
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


        builder.HasOne(oo => oo.ProjectOperationTemporaryDaily)
               .WithMany(oo => oo.ProjectOperationTemporaryDailyDocuments)
               .HasForeignKey("ProjectOperationTemporaryDailyId")
               .HasPrincipalKey(nameof(ProjectOperationTemporaryDaily.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}
