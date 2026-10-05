using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.RequestMachineries;

public class RequestMachineryInquiryOperatorConfiguration : IEntityTypeConfiguration<RequestMachineryInquiryOperator>
{
    private const string _tableName = "RequestMachineryInquiryOperators";
    public void Configure(EntityTypeBuilder<RequestMachineryInquiryOperator> builder)
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

        builder.Property(oo => oo.OperatorAppoinmentId)
            .IsRequired();

        builder.Property(oo => oo.OperatorAppoinmentUserId)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();


        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.InquiryOperators)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
