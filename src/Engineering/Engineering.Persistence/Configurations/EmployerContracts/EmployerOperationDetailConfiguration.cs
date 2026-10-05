using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.CostOvers;

public class EmployerOperationDetailConfiguration : IEntityTypeConfiguration<EmployerOperationDetail>
{
    private const string TableName = "EmployerOperationDetails";
    public void Configure(EntityTypeBuilder<EmployerOperationDetail> builder)
    {
        builder.MetaConfiguration<EmployerOperationDetail, long>(TableName);


        builder.HasOne(oo => oo.EmployerOperation)
            .WithMany(oo => oo.EmployerOperationDetails)
            .HasForeignKey("EmployerOperationId")
            .HasPrincipalKey(nameof(EmployerOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.EmployerOperationDetails)
            .HasForeignKey("ProjectOperationDetailId")
            .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}