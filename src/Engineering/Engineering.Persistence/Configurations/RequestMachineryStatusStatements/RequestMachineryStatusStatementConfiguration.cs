using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Persistence.Configurations.RequestMachineryStatusStatements;

public class RequestMachineryStatusStatementConfiguration : IEntityTypeConfiguration<RequestMachineryStatusStatement>
{
    private const string _tableName = "RequestMachineryStatusStatements";
    public void Configure(EntityTypeBuilder<RequestMachineryStatusStatement> builder)
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

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.BankAccountId);

        builder.Property(oo => oo.PaymentOrderId);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.CostCategoryId)
            .IsRequired(false);

        builder.Property(oo => oo.CostGroupId)
            .IsRequired(false);

        builder.Property(oo => oo.PreferentialTypeId)
            .IsRequired(false);

        builder.Property(oo => oo.DocumentTypeId)
            .IsRequired(false);

        builder.Property(oo => oo.PaymentDate);

        builder.Property(oo => oo.FromDate);

        builder.Property(oo => oo.ToDate);

        builder.Property(oo => oo.TotalRequestedCount)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.TotalFinalPrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.ContractorPrice)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.Status)
            .HasDefaultValue(RequestMachineryStatusStatementStatus.New)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.IBAN)
            .HasColumnType("nvarchar(100)");

        builder.HasOne(oo => oo.Season)
            .WithMany(oo => oo.RequestMachineryStatusStatements)
            .HasForeignKey("SeasonId")
            .HasPrincipalKey(nameof(Season.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
