using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails;

public class ProjectOperationContractorServiceConfiguration : IEntityTypeConfiguration<ProjectOperationDetailContractorService>
{
    private const string _tableName = "ProjectOperationDetailContractorServices";

    public void Configure(EntityTypeBuilder<ProjectOperationDetailContractorService> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.Volume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.TimeSpant)
            .IsRequired();

        builder.Property(oo => oo.RemainingVolume)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasDefaultValue(ContractorServiceStatus.New)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .HasDefaultValue(PODContractorServiceType.ServiceBased)
            .IsRequired();

        builder.Property(x => x.OperationInfoServiceId)
            .IsRequired(false);

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.ProjectOperationDetailContractorServices)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.OperationInfoService)
            .WithMany(x => x.ProjectOperationDetailContractorServices)
            .HasForeignKey(x => x.OperationInfoServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectServiceDetail)
            .WithMany(oo => oo.ProjectOperationDetailContractorServices)
            .HasForeignKey("ProjectServiceDetailId").HasPrincipalKey(nameof(ProjectServiceDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}