using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Persistence.Configurations.RequestContractors;

public class RequestContractorInquiryConfiguration : IEntityTypeConfiguration<RequestContractorInquiry>
{
    private const string _tableName = "RequestContractorInquiries";
    public void Configure(EntityTypeBuilder<RequestContractorInquiry> builder)
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

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.Property(oo => oo.Amount)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.TotalAmount)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.Discount)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.Tax)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.FromDate);

        builder.Property(oo => oo.ToDate);

        builder.Property(oo => oo.IsConfirmed)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.ConfirmedUser);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.RequestContractor)
               .WithMany(oo => oo.Inquiries)
               .HasForeignKey("RequestContractorId")
               .HasPrincipalKey(nameof(RequestContractor.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
