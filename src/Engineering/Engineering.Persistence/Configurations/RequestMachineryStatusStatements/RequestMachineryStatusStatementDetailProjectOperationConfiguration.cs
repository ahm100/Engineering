using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Persistence.Configurations.RequestMachineryStatusStatementDetailProjectOperations;

public class RequestMachineryStatusStatementDetailProjectOperationConfiguration : IEntityTypeConfiguration<RequestMachineryStatusStatementDetailProjectOperation>
{
    private const string _tableName = "RequestMachineryStatusStatementDetailProjectOperations";
    public void Configure(EntityTypeBuilder<RequestMachineryStatusStatementDetailProjectOperation> builder)
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

        builder.HasOne(oo => oo.ProjectOperation)
               .WithMany(oo => oo.StatusStatementDetailProjectOperations)
               .HasForeignKey("ProjectOperationId")
               .HasPrincipalKey(nameof(ProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestMachineryStatusStatementDetail)
               .WithMany(oo => oo.StatusStatementDetailProjectOperations)
               .HasForeignKey("RequestMachineryStatusStatementDetailId")
               .HasPrincipalKey(nameof(RequestMachineryStatusStatementDetail.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
