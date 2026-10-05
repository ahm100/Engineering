using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestHistoryConfiguration : IEntityTypeConfiguration<TransportationRequestHistory>
{
    private const string _tableName = "TransportationRequestHistories";
    public void Configure(EntityTypeBuilder<TransportationRequestHistory> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.PaymentOrderId);

        builder.Property(oo => oo.PaymentDate);

        builder.Property(oo => oo.StartDate);

        builder.Property(oo => oo.EndDate);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.DriverId);

        builder.Property(oo => oo.DriverName);

        builder.Property(oo => oo.AccountNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.BankId);

        builder.Property(oo => oo.CardNumber)
            .HasColumnType("nvarchar(30)");

        builder.Property(oo => oo.AccountName)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.IBAN)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.Price)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.CurrencyUnitId);

        builder.Property(oo => oo.AccountDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.TransportationRequestStatus);

        builder.Property(oo => oo.TransportationPaymentType);

        builder.Property(oo => oo.ManagerDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ConfirmUserId);

        builder.Property(oo => oo.ConfirmDate);

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.TransportationRequest)
            .WithMany(oo => oo.TransportationRequestHistories)
            .HasForeignKey("TransportationRequestId")
            .HasPrincipalKey(nameof(TransportationRequest.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
