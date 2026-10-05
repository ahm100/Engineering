using Engineering.Domain.Entities.ProcesVerbal;

namespace Engineering.Persistence.Configurations.ProcesVerbals;

public class ProcesVerbalDocConfiguration : IEntityTypeConfiguration<ProcesVerbalDoc>
{
    private const string TableName = "ProcesVerbalDocs";

    public void Configure(EntityTypeBuilder<ProcesVerbalDoc> builder)
    {
        builder.MetaConfiguration<ProcesVerbalDoc, long>(TableName);

        builder.Property(oo => oo.URL)
            .HasComment(GlobalCmts.URL)
            .HasColumnType("nvarchar(1000)")
            .HasMaxLength(1000)
            .IsRequired();

        builder.HasOne(oo => oo.ProcesVerbal)
            .WithMany(oo => oo.ProcesVerbalDocuments)
            .HasForeignKey("ProcesVerbalId").HasPrincipalKey(nameof(ProcesVerbal.Id))
            .OnDelete(DeleteBehavior.Cascade);
    }
}