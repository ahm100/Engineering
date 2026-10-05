using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Persistence.Configurations.RequestMachineries;

public class RequestMachineryConfiguration : IEntityTypeConfiguration<RequestMachinery>
{
    private const string _tableName = "RequestMachineries";
    public void Configure(EntityTypeBuilder<RequestMachinery> builder)
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

        builder.Property(oo => oo.RequestNumber);

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.DriverId);

        builder.Property(oo => oo.DriverName);

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.TimeRequired)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.ConfirmedTimeRequired)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Unit)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasDefaultValue(RequestMachineryStatus.New)
            .IsRequired();

        builder.Property(oo => oo.RequestCount)
            .IsRequired();

        builder.Property(oo => oo.FromDate)
            .IsRequired();

        builder.Property(oo => oo.ToDate)
            .IsRequired();

        builder.Property(oo => oo.ConfirmFromDate);

        builder.Property(oo => oo.ConfirmToDate);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ConfirmedDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagerDescription)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.Project)
               .WithMany(oo => oo.RequestMachineries)
               .HasForeignKey("ProjectId")
               .HasPrincipalKey(nameof(Project.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ContractorMachinery)
               .WithMany(oo => oo.RequestMachineries)
               .HasForeignKey("ContractorMachineryId")
               .HasPrincipalKey(nameof(ContractorMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Machinery)
               .WithMany(oo => oo.RequestMachineries)
               .HasForeignKey("MachineryId")
               .HasPrincipalKey(nameof(Machinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
