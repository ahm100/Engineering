namespace Engineering.Application.Services.ServiceInfos.Commands.SetServiceInfoDetail;

public class SetServiceInfoDetailCommandValidator : AbstractValidator<SetServiceInfoDetailCommand>
{
    public SetServiceInfoDetailCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}