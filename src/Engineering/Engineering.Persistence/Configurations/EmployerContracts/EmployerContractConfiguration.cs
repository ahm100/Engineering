using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerContractConfiguration : IEntityTypeConfiguration<EmployerContract>
{
    private const string TableName = "EmployerContracts";
    public void Configure(EntityTypeBuilder<EmployerContract> builder)
    {
        builder.MetaActiveConfiguration<EmployerContract, long>(TableName);

        builder.Property(oo => oo.IsFirst)
            .HasComment(EContractCmts.IsFirst)
            .IsRequired();

        builder.Property(oo => oo.Code)
            .HasComment(GlobalCmts.Code)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.Status)
            .HasComment(EContractCmts.ContractStatus)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .HasComment(GlobalCmts.StartDate);

        builder.Property(oo => oo.EndDate)
            .HasComment(GlobalCmts.EndDate);

        builder.Property(oo => oo.TotalAmount)
            .HasComment(EContractCmts.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.LastDescription)
            .HasComment(GlobalCmts.LastDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.AdvancePayment)
            .HasComment(EContractCmts.AdvancePayment)
            .HasColumnType("decimal(5,2)");


        builder.HasOne(oo => oo.EmployerContractHead)
            .WithMany(oo => oo.EmployerContracts)
            .HasForeignKey("EmployerContractHeadId")
            .HasPrincipalKey(nameof(EmployerContractHead.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.EmployerContracts)
            .HasForeignKey("ProjectId")

            .OnDelete(DeleteBehavior.NoAction);

    }
}