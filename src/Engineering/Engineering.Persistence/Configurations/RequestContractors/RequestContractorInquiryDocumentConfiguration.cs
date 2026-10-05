using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Persistence.Configurations.RequestContractors;

public class RequestContractorInquiryDocumentConfiguration : IEntityTypeConfiguration<RequestContractorInquiryDocument>
{
    private const string _tableName = "RequestContractorInquiryDocuments";
    public void Configure(EntityTypeBuilder<RequestContractorInquiryDocument> builder)
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

        builder.Property(oo => oo.Url)
            .IsRequired();


        builder.HasOne(oo => oo.RequestContractorInquiry)
               .WithMany(oo => oo.RequestContractorInquiryDocuments)
               .HasForeignKey("RequestContractorInquiryId")
               .HasPrincipalKey(nameof(RequestContractorInquiry.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}

