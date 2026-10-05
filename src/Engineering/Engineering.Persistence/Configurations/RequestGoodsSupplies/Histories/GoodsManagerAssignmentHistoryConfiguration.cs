using Engineering.Domain.Entities.GoodsManager;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Histories;

public class GoodsManagerAssignmentHistoryConfiguration : IEntityTypeConfiguration<GoodsManagerAssignmentHistory>
{
    private const string TableName = "GoodsManagerAssignmentHistories";

    public void Configure(EntityTypeBuilder<GoodsManagerAssignmentHistory> builder)
    {
        builder.MetaConfiguration<GoodsManagerAssignmentHistory, long>(TableName);

        builder.Property(oo => oo.OrganizationId)
            .HasComment(RGSCmts.OrganizationId);

        builder.Property(oo => oo.ProductId)
            .HasComment(RGSCmts.Product);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.GoodsManagerAssignment)
               .WithMany(oo => oo.Histories)
               .HasForeignKey(oo => oo.GoodsManagerAssignmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}