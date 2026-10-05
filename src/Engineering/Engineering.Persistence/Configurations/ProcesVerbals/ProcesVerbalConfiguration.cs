using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.ProcesVerbals;

public class ProcesVerbalConfiguration : IEntityTypeConfiguration<ProcesVerbal>
{
    private const string TableName = "ProcesVerbals";

    public void Configure(EntityTypeBuilder<ProcesVerbal> builder)
    {
        builder.MetaActiveConfiguration<ProcesVerbal, long>(TableName);

        builder.Property(oo => oo.TitleFa)
            .HasComment(ProcesVerbalCmts.TitleFa)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.TitleEn)
            .HasComment(ProcesVerbalCmts.TitleEn)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.Type)
            .HasComment(ProcesVerbalCmts.Type)
            .IsRequired();

        builder.Property(oo => oo.RecordDateTime)
            .HasComment(ProcesVerbalCmts.RecordDateTime)
            .IsRequired();

        builder.Property(oo => oo.Location)
            .HasComment(ProcesVerbalCmts.Location)
            .HasColumnType("nvarchar(500)")
            .HasMaxLength(500);

        builder.Property(oo => oo.DeliveryStatus)
            .HasComment(ProcesVerbalCmts.DeliveryStatus);

        builder.Property(oo => oo.Limitations)
            .HasComment(ProcesVerbalCmts.Limitations)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ProductStatus)
            .HasComment(ProcesVerbalCmts.ProcesVerbalProductItemStatus);

        builder.Property(oo => oo.WorkStatus)
            .HasComment(ProcesVerbalCmts.WorkStatus);

        builder.Property(oo => oo.LimitationStatus)
            .HasComment(ProcesVerbalCmts.LimitationStatus);

        builder.Property(oo => oo.WorkStartStatus)
            .HasComment(ProcesVerbalCmts.WorkStartStatus);

        builder.Property(oo => oo.WorkStopReason)
            .HasComment(ProcesVerbalCmts.WorkStopReason);

        builder.Property(oo => oo.WorkStopStatus)
            .HasComment(ProcesVerbalCmts.WorkStopStatus);

        builder.HasOne(oo => oo.Contract)
            .WithMany(oo => oo.ProcesVerbals)
            .HasForeignKey("ContractId").HasPrincipalKey(nameof(Domain.Entities.Contracts.Contract.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProcesVerbals)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}