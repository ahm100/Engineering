using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Configurations.OperationInfos;

public class OperationInfoActionConfiguration : IEntityTypeConfiguration<OperationInfoAction>
{
    private const string TableName = "OperationInfoActions";
    public void Configure(EntityTypeBuilder<OperationInfoAction> builder)
    {
        builder.MetaConfiguration<OperationInfoAction, long>(TableName);

        builder.Property(oo => oo.Price)
            .HasComment(ProjectOperationCmts.Price)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(a => a.OperationInfo)
            .WithMany(a => a.OperationInfoActions)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Action)
            .WithMany(a => a.OperationInfoActions)
            .HasForeignKey("ActionId").HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}