using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerDocUrlConfiguration : IEntityTypeConfiguration<EmployerDocUrl>
{
    private const string TableName = "EmployerDocUrls";
    public void Configure(EntityTypeBuilder<EmployerDocUrl> builder)
    {
        builder.MetaConfiguration<EmployerDocUrl, long>(TableName);

        builder.Property(oo => oo.URL)
            .HasComment(GlobalCmts.URL)
            .HasColumnType("nvarchar(1500)")
            .IsRequired();

        builder.HasOne(oo => oo.EmployerDoc)
            .WithMany(oo => oo.EmployerDocUrls)
            .HasForeignKey("EmployerDocId")
            .HasPrincipalKey(nameof(EmployerDoc.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}