using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Persistence.Configurations;

public class ContractorServiceConfiguration : IEntityTypeConfiguration<ContractorService>
{
    private const string _tableName = "ContractorServices";
    public void Configure(EntityTypeBuilder<ContractorService> builder)
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

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.ServiceInfoId)
            .IsRequired();
    }
}