using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.ProjectUsers;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectThirdPartyConfiguration : IEntityTypeConfiguration<ProjectThirdParty>
{
    private const string TableName = "ProjectThirdParties";
    public void Configure(EntityTypeBuilder<ProjectThirdParty> builder)
    {
        builder.MetaConfiguration<ProjectThirdParty, long>(TableName);

        builder.Property(oo => oo.AuthorizedThirdPartyId)
            .HasComment(ProjectCmts.AuthorizedThirdPartyId)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectThirdParties)
            .HasForeignKey("ProjectId")
            .HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}