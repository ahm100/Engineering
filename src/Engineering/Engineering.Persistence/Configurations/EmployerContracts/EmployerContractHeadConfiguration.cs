using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerContractHeadConfiguration : IEntityTypeConfiguration<EmployerContractHead>
{
    private const string TableName = "EmployerContractHeads";
    public void Configure(EntityTypeBuilder<EmployerContractHead> builder)
    {
        builder.MetaConfiguration<EmployerContractHead, long>(TableName);

        builder.Property(oo => oo.Code)
            .HasColumnType("nvarchar(250)")
            .HasComment(GlobalCmts.Code)
            .HasMaxLength(250);

        builder.Property(oo => oo.VolumeTolerance)
            .HasColumnType("decimal(18,2)")
            .HasComment(EContractCmts.VolumeTolerance)
            .IsRequired(false);

        builder.Property(oo => oo.PriceTolerance)
            .HasColumnType("decimal(18,2)")
            .HasComment(EContractCmts.PriceTolerance)
            .IsRequired(false);

        builder.Property(oo => oo.EmployerId)
            .HasComment(EContractCmts.EmployerId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .HasComment(EContractCmts.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .HasComment(GlobalCmts.StartDate);

        builder.Property(oo => oo.EndDate)
            .HasComment(GlobalCmts.EndDate);

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .HasComment(EContractCmts.EContractType)
            .IsRequired();


        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.EmployerContractHeads)
            .HasForeignKey("CostCenterId")
            .HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}