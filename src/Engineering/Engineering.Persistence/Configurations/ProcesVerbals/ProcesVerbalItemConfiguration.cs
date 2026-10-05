using Engineering.Domain.Entities.ProcesVerbal;

namespace Engineering.Persistence.Configurations.ProcesVerbals;

public class ProcesVerbalItemConfiguration : IEntityTypeConfiguration<ProcesVerbalItem>
{
    private const string TableName = "ProcesVerbalItems";

    public void Configure(EntityTypeBuilder<ProcesVerbalItem> builder)
    {
        builder.MetaConfiguration<ProcesVerbalItem, long>(TableName);

        builder.Property(oo => oo.TitleFa)
            .HasComment(ProcesVerbalCmts.ProcesVerbalItemTitle)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(oo => oo.ProcesVerbal)
            .WithMany(oo => oo.ProcesVerbalItems)
            .HasForeignKey("ProcesVerbalId").HasPrincipalKey(nameof(ProcesVerbal.Id))
            .OnDelete(DeleteBehavior.Cascade);
    }
}