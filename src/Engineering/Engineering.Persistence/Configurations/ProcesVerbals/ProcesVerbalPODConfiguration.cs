using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.ProcesVerbals;

public class ProcesVerbalPODConfiguration : IEntityTypeConfiguration<ProcesVerbalPOD>
{
    private const string TableName = "ProcesVerbalPODs";

    public void Configure(EntityTypeBuilder<ProcesVerbalPOD> builder)
    {
        builder.MetaConfiguration<ProcesVerbalPOD, long>(TableName);

        builder.Property(oo => oo.NewFinalAmount)
            .HasComment(ProcesVerbalCmts.NewFinalAmout)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne(oo => oo.ProcesVerbal)
            .WithMany(oo => oo.ProcesVerbalPODs)
            .HasForeignKey("ProcesVerbalId").HasPrincipalKey(nameof(ProcesVerbal.Id))
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.ProcesVerbalPODs)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}