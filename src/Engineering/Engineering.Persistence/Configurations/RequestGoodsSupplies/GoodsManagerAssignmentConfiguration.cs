using Engineering.Domain.Entities.GoodsManager;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies;

public class GoodsManagerAssignmentConfiguration : IEntityTypeConfiguration<GoodsManagerAssignment>
{
    private const string TableName = "GoodsManagerAssignments";

    public void Configure(EntityTypeBuilder<GoodsManagerAssignment> builder)
    {
        builder.MetaConfiguration<GoodsManagerAssignment, long>(TableName);

        builder.Property(x => x.OrganizationId)
            .HasComment(RGSCmts.OrganizationId)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasComment(RGSCmts.Product);

    }
}