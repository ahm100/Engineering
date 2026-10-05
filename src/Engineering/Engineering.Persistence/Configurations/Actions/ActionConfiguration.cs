
using Action = Engineering.Domain.Entities.Actions.Action;

namespace Engineering.Persistence.Configurations.Actions;

public class ActionConfiguration : IEntityTypeConfiguration<Action>
{
    private const string TableName = "Actions";
    public void Configure(EntityTypeBuilder<Action> builder)
    {
        builder.MetaActiveConfiguration<Action, long>(TableName);

        builder.Property(oo => oo.ActionName)
            .HasComment(GlobalCmts.ActionName)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.ActionCode)
            .HasComment(GlobalCmts.ActionCode)
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

    }
}