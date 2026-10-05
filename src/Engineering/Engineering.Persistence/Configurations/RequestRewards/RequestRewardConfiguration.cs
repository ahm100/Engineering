using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Persistence.Configurations.RequestRewards;

public class RequestRewardConfiguration : IEntityTypeConfiguration<RequestReward>
{
    private const string _tableName = "RequestRewards";
    public void Configure(EntityTypeBuilder<RequestReward> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.CurrencyId);

        builder.Property(oo => oo.Status)
            .HasDefaultValue(RequestRewardStatus.New)
            .IsRequired();

        builder.Property(oo => oo.Type)
           .IsRequired();

        builder.Property(oo => oo.OfferedPrice)
           .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.ConfirmedPrice)
           .HasColumnType("decimal(18, 2)")
           .IsRequired();

        builder.Property(oo => oo.RegistrationDate)
           .IsRequired();

        builder.Property(oo => oo.ManagerDescription)
           .HasColumnType("nvarchar(1500)");


        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.RequestRewards)
            .HasForeignKey("ProjectOperationDetailId")
            .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.RequestRewards)
            .HasForeignKey("CostCenterId")
            .HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.RequestRewards)
            .HasForeignKey("ProjectOperationId")
            .HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.RequestRewards)
            .HasForeignKey("ProjectId")
            .HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.Restrict);

    }
}
