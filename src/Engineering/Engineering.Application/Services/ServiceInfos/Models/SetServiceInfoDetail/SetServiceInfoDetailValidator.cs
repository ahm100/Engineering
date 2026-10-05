namespace Engineering.Application.Services.ServiceInfos.Models.SetServiceInfoDetail;

public class SetServiceInfoDetailValidator : AbstractValidator<SetServiceInfoDetailRequest>
{
    public SetServiceInfoDetailValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}