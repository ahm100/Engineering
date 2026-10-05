using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Persistence.Configurations.RequestContractors;

public class RequestContractorHistoryConfiguration : IEntityTypeConfiguration<RequestContractorHistory>
{
    private const string _tableName = "RequestContractorHistories";
    public void Configure(EntityTypeBuilder<RequestContractorHistory> builder)
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
            .HasDefaultValue(RequestContractorStatus.New)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.StatusDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Volume)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0)
               .IsRequired();

        builder.HasOne(oo => oo.RequestContractor)
               .WithMany(oo => oo.Histories)
               .HasForeignKey("RequestContractorId")
               .HasPrincipalKey(nameof(RequestContractor.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
