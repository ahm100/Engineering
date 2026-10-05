using Engineering.Domain.Entities.EmployerEmployees;

namespace Engineering.Persistence.Configurations.EmployerEmployees;

public class EmployerEmployeeConfiguration : IEntityTypeConfiguration<EmployerEmployee>
{
    private const string _tableName = "EmployerEmployees";
    public void Configure(EntityTypeBuilder<EmployerEmployee> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.EmployerId)
            .IsRequired();

        builder.Property(oo => oo.EmployeeId)
            .IsRequired();

    }
}
