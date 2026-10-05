using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.RequestMachineries;

public class RequestMachineryInquiryConfiguration : IEntityTypeConfiguration<RequestMachineryInquiry>
{
    private const string _tableName = "RequestMachineryInquiries";
    public void Configure(EntityTypeBuilder<RequestMachineryInquiry> builder)
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
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.ThirdPartyId)
            .IsRequired();

        builder.Property(oo => oo.Count)
            .IsRequired();

        builder.Property(oo => oo.Unit)
            .IsRequired();

        builder.Property(oo => oo.UnitPrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.TotalPrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.IsConfirmed)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.InquiryRequestedTime)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne(oo => oo.RequestMachineryInquiryOperator)
               .WithMany(oo => oo.Inquiries)
               .HasForeignKey("RequestMachineryInquiryOperatorId")
               .HasPrincipalKey(nameof(RequestMachineryInquiryOperator.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
