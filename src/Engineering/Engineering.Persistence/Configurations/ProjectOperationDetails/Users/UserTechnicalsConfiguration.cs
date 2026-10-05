using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails.Users;

public class UserTechnicalsConfiguration : IEntityTypeConfiguration<UserTechnical>
{
    private const string _tableName = "ProjectOperationDetailUserTechnicals";
    public void Configure(EntityTypeBuilder<UserTechnical> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.TechnicalAssistantUserId)
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
            .WithMany(oo => oo.UserTechnicals)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}