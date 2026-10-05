using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails.Users;

public class UserImplementationsConfiguration : IEntityTypeConfiguration<UserImplementation>
{
    private const string _tableName = "ProjectOperationDetailUserImplementations";
    public void Configure(EntityTypeBuilder<UserImplementation> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ImplementationAssistantUserId)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.UserImplementations)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}