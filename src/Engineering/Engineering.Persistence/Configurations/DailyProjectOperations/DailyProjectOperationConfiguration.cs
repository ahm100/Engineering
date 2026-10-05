using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.DailyProjectOperations;

public class DailyProjectOperationConfiguration : IEntityTypeConfiguration<DailyProjectOperation>
{
    private const string _tableName = "DailyProjectOperations";
    public void Configure(EntityTypeBuilder<DailyProjectOperation> builder)
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

        builder.Property(oo => oo.LegacyId);

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.Length)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Width)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Height)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Weight)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Number)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .HasDefaultValue(DailyProjectOperationType.Daily)
            .IsRequired();

        builder.Ignore(c => c.FinalAmount);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(c => c.ProjectOperationDetail)
               .WithMany(c => c.DailyOperations)
               .HasForeignKey("ProjectOperationDetailId")
               .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.DailyProjectOperationDocuments)
               .WithOne(c => c.DailyProjectOperation)
               .HasForeignKey("DailyProjectOperationId")
               .HasPrincipalKey(nameof(DailyProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.DailyProjectOperationExperts)
               .WithOne(c => c.DailyProjectOperation)
               .HasForeignKey("DailyProjectOperationId")
               .HasPrincipalKey(nameof(DailyProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.DailyProjectOperationMachineries)
               .WithOne(c => c.DailyProjectOperation)
               .HasForeignKey("DailyProjectOperationId")
               .HasPrincipalKey(nameof(DailyProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.DailyProjectOperationProducts)
               .WithOne(c => c.DailyProjectOperation)
               .HasForeignKey("DailyProjectOperationId")
               .HasPrincipalKey(nameof(DailyProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.DailyProjectOperationServices)
               .WithOne(c => c.DailyProjectOperation)
               .HasForeignKey("DailyProjectOperationId")
               .HasPrincipalKey(nameof(DailyProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
