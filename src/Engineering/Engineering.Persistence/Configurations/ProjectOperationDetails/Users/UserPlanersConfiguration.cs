using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails.Users;

public class UserPlanersConfiguration : IEntityTypeConfiguration<UserPlaner>
{
    private const string _tableName = "ProjectOperationDetailUserPlaners";
    public void Configure(EntityTypeBuilder<UserPlaner> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.UserPlanerId)
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
            .WithMany(oo => oo.UserPlaners)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}