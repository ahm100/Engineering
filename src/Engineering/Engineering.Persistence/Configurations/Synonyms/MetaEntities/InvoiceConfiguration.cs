using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class ViewInvoiceConfiguration : IEntityTypeConfiguration<ViewInvoice>
{
    public void Configure(EntityTypeBuilder<ViewInvoice> builder)
    {
        builder.ToView("ViewInvoice", "engineer");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Warehouse)
       .WithMany()
       .HasForeignKey(x => x.WarehouseId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DestinationWarehouse)
       .WithMany()
       .HasForeignKey(x => x.DestinationWarehouseId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.FinalWarehouse)
       .WithMany()
       .HasForeignKey(x => x.FinalWarehouseId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(e => e.InvoiceProducts);
    }
}