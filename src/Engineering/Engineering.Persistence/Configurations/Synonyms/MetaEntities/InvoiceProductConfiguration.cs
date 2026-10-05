using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class ViewInvoiceProductConfiguration : IEntityTypeConfiguration<ViewInvoiceProduct>
{
    public void Configure(EntityTypeBuilder<ViewInvoiceProduct> builder)
    {
        builder.ToView("ViewInvoiceProduct", "engineer");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Package)
       .WithMany()
       .HasForeignKey(x => x.PackageId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Invoice)
       .WithMany()
       .HasForeignKey(x => x.InvoiceId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Product)
       .WithMany()
       .HasForeignKey(x => x.ProductId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(x => x.Invoice);

        builder.Ignore(x => x.PricedInvoiceProductPrices);
        // builder.HasMany(x => x.InvoiceProductPrices)
        //.WithOne(x => x.InvoiceProduct)
        //.HasForeignKey(x => x.InvoiceProductId)
        //.HasPrincipalKey(x => x.Id)
        //.OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(x => x.PricedInvoiceProductPrices);
        // builder.HasMany(x => x.PricedInvoiceProductPrices)
        //.WithOne(x => x.PricedInvoiceProduct)
        //.HasForeignKey(x => x.PricedInvoiceProductId)
        //.HasPrincipalKey(x => x.Id)
        //.OnDelete(DeleteBehavior.Restrict);
    }
}