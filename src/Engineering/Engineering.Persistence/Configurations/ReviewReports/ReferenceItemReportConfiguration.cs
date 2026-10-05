using Engineering.Domain.Entities.ReviewReports;

namespace Engineering.Persistence.Configurations.ReviewReports;

public class ReferenceItemReportConfiguration : IEntityTypeConfiguration<ReferenceItemReport>
{
    public void Configure(EntityTypeBuilder<ReferenceItemReport> builder)
    {
        builder.HasNoKey();
        builder.ToView("vwReferenceItemReport", "engineer");

    }
}