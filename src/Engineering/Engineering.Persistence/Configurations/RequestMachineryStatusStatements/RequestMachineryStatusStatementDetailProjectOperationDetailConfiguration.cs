using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Persistence.Configurations.RequestMachineryStatusStatementDetailProjectOperationDetails;

public class RequestMachineryStatusStatementDetailProjectOperationDetailConfiguration : IEntityTypeConfiguration<RequestMachineryStatusStatementDetailProjectOperationDetail>
{
    private const string _tableName = "RequestMachineryStatusStatementDetailProjectOperationDetails";
    public void Configure(EntityTypeBuilder<RequestMachineryStatusStatementDetailProjectOperationDetail> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
        .ValueGeneratedOnAdd().
        IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.ProjectOperationDetail)
                .WithMany(oo => oo.StatusStatementDetailProjectOperationDetails)
                .HasForeignKey("ProjectOperationDetailId")
                .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestMachineryStatusStatementDetail)
               .WithMany(oo => oo.StatusStatementDetailProjectOperationDetails)
               .HasForeignKey("RequestMachineryStatusStatementDetailId")
               .HasPrincipalKey(nameof(RequestMachineryStatusStatementDetail.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
