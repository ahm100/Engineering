using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.DailyProjectOperations;

public class DailyProjectOperationServiceConfiguration : IEntityTypeConfiguration<DailyProjectOperationService>
{
    private const string _tableName = "DailyProjectOperationServices";
    public void Configure(EntityTypeBuilder<DailyProjectOperationService> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.ThirdPartyId);

        builder.Property(oo => oo.ProjectServiceVolume)
            .HasColumnType("decimal(18,5)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(c => c.ProjectOperationDetailContractorService)
               .WithMany(c => c.DailyOperationServices)
               .HasForeignKey("ContractorServiceId")
               .HasPrincipalKey(nameof(ProjectOperationDetailContractorService.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}

