using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Persistence.Configurations.RequestMachineryStatusStatementDetails;

public class RequestMachineryStatusStatementDetailConfiguration : IEntityTypeConfiguration<RequestMachineryStatusStatementDetail>
{
    private const string _tableName = "RequestMachineryStatusStatementDetails";
    public void Configure(EntityTypeBuilder<RequestMachineryStatusStatementDetail> builder)
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

        builder.Property(oo => oo.CurrencyId);

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.FromDate);

        builder.Property(oo => oo.ToDate);

        builder.Property(oo => oo.RequestedCount)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.FinalPrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.OperatorId);

        builder.Property(oo => oo.TimeRequired)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.Unit)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
               .WithMany(oo => oo.RequestMachineryStatusStatementDetails)
               .HasForeignKey("ProjectId")
               .HasPrincipalKey(nameof(Project.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Machinery)
               .WithMany(oo => oo.RequestMachineryStatusStatementDetails)
               .HasForeignKey("MachineryId")
               .HasPrincipalKey(nameof(Machinery.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.RequestMachineryStatusStatementDetails)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestMachineryStatusStatement)
               .WithMany(oo => oo.RequestMachineryStatusStatementDetails)
               .HasForeignKey("RequestMachineryStatusStatementId")
               .HasPrincipalKey(nameof(RequestMachineryStatusStatement.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
