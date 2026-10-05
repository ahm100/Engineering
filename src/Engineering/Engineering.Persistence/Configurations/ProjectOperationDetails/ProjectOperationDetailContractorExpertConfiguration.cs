using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails;

public class ProjectOperationDetailContractorExpertConfiguration : IEntityTypeConfiguration<ProjectOperationDetailContractorExpert>
{
    private const string TableName = "ProjectOperationDetailContractorExperts";
    public void Configure(EntityTypeBuilder<ProjectOperationDetailContractorExpert> builder)
    {
        builder.MetaActiveConfiguration<ProjectOperationDetailContractorExpert, long>(TableName);

        builder.Property(oo => oo.Volume)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.HaveContract)
            .IsRequired();

        builder.HasOne(oo => oo.ConsumableVolumeExpert)
            .WithMany(oo => oo.ProjectOperationDetailContractorExperts)
            .HasForeignKey(oo => oo.ConsumableVolumeExpertId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(oo => oo.ProjectOperationDetailContractorService)
            .WithMany(oo => oo.ProjectOperationDetailContractorExperts)
            .HasForeignKey(oo => oo.ProjectOperationDetailContractorServiceId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}