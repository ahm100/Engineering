using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class ShippingCostHistoryConfiguration : IEntityTypeConfiguration<ShippingCostHistory>
{
    private const string _tableName = "ShippingCostHistories";
    public void Configure(EntityTypeBuilder<ShippingCostHistory> builder)
    {
        builder.MetaActiveConfiguration<ShippingCostHistory, long>(_tableName);

        builder.Property(a => a.Description)
            .HasComment(GlobalCmts.Description);

        builder.Property(a => a.Count)
            .HasComment(ShippingCostCmts.Count)
            .IsRequired(false);

        builder.Property(a => a.FromDate)
            .HasComment(ShippingCostCmts.FromDate);

        builder.Property(a => a.ToDate)
            .HasComment(ShippingCostCmts.ToDate);

        builder.Property(a => a.LoadWeight)
            .HasComment(ShippingCostCmts.LoadWeight)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);

        builder.Property(a => a.Price)
            .HasComment(ShippingCostCmts.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(a => a.ThirdPartyCompanyId)
            .HasComment(ShippingCostCmts.ThirdPartyCompany);

        builder.Property(a => a.Tax)
            .HasComment(ShippingCostCmts.Tax)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);

        builder.Property(a => a.Latitude)
            .HasComment(ShippingCostCmts.Latitude)
            .HasColumnType("decimal(18,9)")
            .IsRequired(false);

        builder.Property(a => a.Longitude)
            .HasComment(ShippingCostCmts.Longitude)
            .HasColumnType("decimal(18,9)")
            .IsRequired(false);

        builder.HasOne(x => x.ShippingCost)
       .WithMany(x => x.ShippingCostHistories)
       .HasPrincipalKey(x => x.Id)
       .HasForeignKey(x => x.ShippingCostId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.MachineType)
       .WithMany(x => x.ShippingCostHistories)
       .HasPrincipalKey(x => x.Id)
       .HasForeignKey(x => x.MachineTypeId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ThirdParty)
       .WithMany()
       .HasForeignKey(x => x.ThirdPartyId)
       .OnDelete(DeleteBehavior.Restrict);

        // builder.HasOne(x => x.Creator)
        //.WithMany()
        //.HasPrincipalKey(x => x.UserId)
        //.HasForeignKey(x => x.CreatorId)
        //.OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Region)
       .WithMany()
       .HasForeignKey(x => x.RegionId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SourceCity)
       .WithMany()
       .HasForeignKey(x => x.SourceCityId)
       .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(x => x.DestinationCity)
       .WithMany()
       .HasForeignKey(x => x.DestinationCityId)
       .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
