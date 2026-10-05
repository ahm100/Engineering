using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.RequestMachineries;

public class RequestMachineryProjectOperationDetailConfiguration : IEntityTypeConfiguration<RequestMachineryProjectOperationDetail>
{
    private const string _tableName = "RequestMachineryProjectOperationDetails";
    public void Configure(EntityTypeBuilder<RequestMachineryProjectOperationDetail> builder)
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
               .WithMany(oo => oo.RequestMachineryProjectOperationDetails)
               .HasForeignKey("ProjectOperationDetailId")
               .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.ProjectOperationDetails)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
