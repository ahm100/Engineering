using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Persistence.Configurations.RequestMachineries;

public class RequestMachineryHistoryConfiguration : IEntityTypeConfiguration<RequestMachineryHistory>
{
    private const string _tableName = "RequestMachineryHistories";
    public void Configure(EntityTypeBuilder<RequestMachineryHistory> builder)
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

        builder.Property(oo => oo.Status)
            .HasDefaultValue(RequestMachineryStatus.New)
            .IsRequired();

        builder.Property(oo => oo.ConfirmedDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ConfirmFromDate);

        builder.Property(oo => oo.ConfirmToDate);

        builder.Property(oo => oo.ConfirmedTimeRequired);

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.MachineryId);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");


        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.Histories)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
