using Engineering.Domain.Entities.OperationInfoHistorys;

namespace Engineering.Persistence.Configurations.OperationInfos;

public class OperationInfoHistoryConfiguration : IEntityTypeConfiguration<OperationInfoHistory>
{
    private const string _tableName = "OperationInfoHistories";
    public void Configure(EntityTypeBuilder<OperationInfoHistory> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.OperationInfoName)
            .HasColumnType("nvarchar(max)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.OperationInfoCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(oo => oo.UnitOfMeasurementId)
            .IsRequired();

        builder.Property(oo => oo.OperationLatinName)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.Priority);

        builder.Property(oo => oo.BasePrice)
            .HasComment(OperationInfoCmts.BasePrice)
            .IsRequired()
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created).ValueGeneratedOnAdd().IsRequired();
        builder.Property(oo => oo.CreatorId).IsRequired();

        builder.HasOne(a => a.OperationInfo)
            .WithMany(a => a.OperationInfoHistories)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}