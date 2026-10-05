using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class ViewInvoiceProductPriceConfiguration : IEntityTypeConfiguration<ViewInvoiceProductPrice>
{
    public void Configure(EntityTypeBuilder<ViewInvoiceProductPrice> builder)
    {
        builder.ToView("ViewInvoiceProductPrice", "engineer");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.InvoiceProduct)
       .WithMany()
       .HasForeignKey(x => x.InvoiceProductId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PricedInvoiceProduct)
       .WithMany()
       .HasForeignKey(x => x.PricedInvoiceProductId)
       .OnDelete(DeleteBehavior.Restrict);
    }
}