using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Persistence.Configurations;

public class ContractorEmployeeConfiguration : IEntityTypeConfiguration<ContractorEmployee>
{
    private const string _tableName = "ContractorEmployees";
    public void Configure(EntityTypeBuilder<ContractorEmployee> builder)
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

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.EmployeeId)
            .IsRequired();

    }
}
